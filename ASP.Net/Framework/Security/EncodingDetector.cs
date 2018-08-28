using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Framework
{
    ///<summary>
  /// EncodingDetector. Detects the Encoding used in byte arrays
  /// or files by testing the start of the file for a Byte Order Mark
  /// (called 'preamble' in .NET).
  ///
  /// Use ReadAllText() to read a file using a detected encoding.
  ///
  /// All encodings that have a preamble are supported.
  ///</summary>
    public class EncodingDetector
    {
        ///<summary>
        /// Helper class to store information about encodings
        /// with a preamble
        ///</summary>
        protected class PreambleInfo
        {
            protected Encoding _encoding;
            protected byte[] _preamble;

            ///<summary>
            /// Property Encoding (Encoding).
            ///</summary>
            public Encoding Encoding
            {
                get { return this._encoding; }
            }

            ///<summary>
            /// Property Preamble (byte[]).
            ///</summary>
            public byte[] Preamble
            {
                get { return this._preamble; }
            }

            ///<summary>
            /// Constructor with preamble and encoding
            ///</summary>
            ///<param name="encoding"></param>
            ///<param name="preamble"></param>
            public PreambleInfo(Encoding encoding, byte[] preamble)
            {
                this._encoding = encoding;
                this._preamble = preamble;
            }
        }

        // The list of encodings with a preamble,
        // sorted longest preamble first.
        protected static SortedList<int, PreambleInfo> _preambles = null;

        // Maximum length of all preamles
        protected static int _maxPreambleLength = 0;

        ///<summary>
        /// Read the contents of a text file as a string. Scan for a preamble first.
        /// If a preamble is found, the corresponding encoding is used.
        /// If no preamble is found, the supplied defaultEncoding is used.
        ///</summary>
        ///<param name="filename">The name of the file to read</param>
        ///<param name="defaultEncoding">The encoding to use if no preamble present</param>
        ///<param name="usedEncoding">The actual encoding used</param>
        ///<returns>The contents of the file as a string</returns>
        public static string ReadAllText(string filename, Encoding defaultEncoding, out Encoding usedEncoding)
        {
            // Read the contents of the file as an array of bytes
            byte[] bytes = File.ReadAllBytes(filename);

            // Detect the encoding of the file:
            usedEncoding = DetectEncoding(bytes);

            // If none found, use the default encoding.
            // Otherwise, determine the length of the encoding markers in the file
            int offset;
            if (usedEncoding == null)
            {
                offset = 0;
                usedEncoding = defaultEncoding;
            }
            else
            {
                offset = usedEncoding.GetPreamble().Length;
            }

            // Now interpret the bytes according to the encoding,
            // skipping the preample (if any)
            return usedEncoding.GetString(bytes, offset, bytes.Length - offset);
        }

        ///<summary>
        /// Detect the encoding in an array of bytes.
        ///</summary>
        ///<param name="bytes"></param>
        ///<returns>The encoding found, or null</returns>
        public static Encoding DetectEncoding(byte[] bytes)
        {
            // Scan for encodings if we haven't done so
            if (_preambles == null)
                ScanEncodings();

            // Try each preamble in turn
            foreach (PreambleInfo info in _preambles.Values)
            {
                // Match all bytes in the preamble
                bool match = true;

                if (bytes.Length >= info.Preamble.Length)
                {
                    for (int i = 0; i < info.Preamble.Length; i++)
                    {
                        if (bytes[i] != info.Preamble[i])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        return info.Encoding;
                    }
                }
            }

            return null;
        }

        ///<summary>
        /// Detect the encoding of a file. Reads just enough of
        /// the file to be able to detect a preamble.
        ///</summary>
        ///<param name="filename">The path name of the file</param>
        ///<returns>The encoding detected, or null if no preamble found</returns>
        public static Encoding DetectEncoding(string filename)
        {
            // Scan for encodings if we haven't done so
            if (_preambles == null)
                ScanEncodings();

            using (FileStream stream = File.OpenRead(filename))
            {
                // Never read more than the length of the file
                // or the maximum preamble length
                long n = stream.Length;

                // No bytes? No encoding!
                if (n == 0)
                    return null;

                // Read the minimum amount necessary
                if (n > _maxPreambleLength)
                    n = _maxPreambleLength;

                byte[] bytes = new byte[n];

                stream.Read(bytes, 0, (int)n);

                // Detect the encoding from the byte array
                return DetectEncoding(bytes);
            }
        }

        public static Encoding DetectEncodingByString(string text)
        {
            try
            {
                return DetectEncoding(Encoding.Default.GetBytes(text));
            }
            catch
            {
                return null;
            }
        }

        // Function to detect the encoding for UTF-7, UTF-8/16/32 (bom, no bom, little
        // & big endian), and local default codepage, and potentially other codepages.
        // 'taster' = number of bytes to check of the file (to save processing). Higher
        // value is slower, but more reliable (especially UTF-8 with special characters
        // later on may appear to be ASCII initially). If taster = 0, then taster
        // becomes the length of the file (for maximum reliability). 'text' is simply
        // the string with the discovered encoding applied to the file.
        public static Encoding DetectTextEncoding(string text, int taster = 1000)
        {
            
            byte[] b = null;
            try
            {
                b = Encoding.Default.GetBytes(text);
            }
            catch { }
            

            //////////////// First check the low hanging fruit by checking if a
            //////////////// BOM/signature exists (sourced from http://www.unicode.org/faq/utf_bom.html#bom4)
            if (b.Length >= 4 && b[0] == 0x00 && b[1] == 0x00 && b[2] == 0xFE && b[3] == 0xFF) { text = Encoding.GetEncoding("utf-32BE").GetString(b, 4, b.Length - 4); return Encoding.GetEncoding("utf-32BE"); }  // UTF-32, big-endian 
            else if (b.Length >= 4 && b[0] == 0xFF && b[1] == 0xFE && b[2] == 0x00 && b[3] == 0x00) { text = Encoding.UTF32.GetString(b, 4, b.Length - 4); return Encoding.UTF32; }    // UTF-32, little-endian
            else if (b.Length >= 2 && b[0] == 0xFE && b[1] == 0xFF) { text = Encoding.BigEndianUnicode.GetString(b, 2, b.Length - 2); return Encoding.BigEndianUnicode; }     // UTF-16, big-endian
            else if (b.Length >= 2 && b[0] == 0xFF && b[1] == 0xFE) { text = Encoding.Unicode.GetString(b, 2, b.Length - 2); return Encoding.Unicode; }              // UTF-16, little-endian
            else if (b.Length >= 3 && b[0] == 0xEF && b[1] == 0xBB && b[2] == 0xBF) { text = Encoding.UTF8.GetString(b, 3, b.Length - 3); return Encoding.UTF8; } // UTF-8
            else if (b.Length >= 3 && b[0] == 0x2b && b[1] == 0x2f && b[2] == 0x76) { text = Encoding.UTF7.GetString(b,3,b.Length-3); return Encoding.UTF7; } // UTF-7


            //////////// If the code reaches here, no BOM/signature was found, so now
            //////////// we need to 'taste' the file to see if can manually discover
            //////////// the encoding. A high taster value is desired for UTF-8
            if (taster == 0 || taster > b.Length) taster = b.Length;    // Taster size can't be bigger than the filesize obviously.


            // Some text files are encoded in UTF8, but have no BOM/signature. Hence
            // the below manually checks for a UTF8 pattern. This code is based off
            // the top answer at: http://stackoverflow.com/questions/6555015/check-for-invalid-utf8
            // For our purposes, an unnecessarily strict (and terser/slower)
            // implementation is shown at: http://stackoverflow.com/questions/1031645/how-to-detect-utf-8-in-plain-c
            // For the below, false positives should be exceedingly rare (and would
            // be either slightly malformed UTF-8 (which would suit our purposes
            // anyway) or 8-bit extended ASCII/UTF-16/32 at a vanishingly long shot).
            int i = 0;
            bool utf8 = false;
            while (i < taster - 4)
            {
                if (b[i] <= 0x7F) { i += 1; continue; }     // If all characters are below 0x80, then it is valid UTF8, but UTF8 is not 'required' (and therefore the text is more desirable to be treated as the default codepage of the computer). Hence, there's no "utf8 = true;" code unlike the next three checks.
                if (b[i] >= 0xC2 && b[i] <= 0xDF && b[i + 1] >= 0x80 && b[i + 1] < 0xC0) { i += 2; utf8 = true; continue; }
                if (b[i] >= 0xE0 && b[i] <= 0xF0 && b[i + 1] >= 0x80 && b[i + 1] < 0xC0 && b[i + 2] >= 0x80 && b[i + 2] < 0xC0) { i += 3; utf8 = true; continue; }
                if (b[i] >= 0xF0 && b[i] <= 0xF4 && b[i + 1] >= 0x80 && b[i + 1] < 0xC0 && b[i + 2] >= 0x80 && b[i + 2] < 0xC0 && b[i + 3] >= 0x80 && b[i + 3] < 0xC0) { i += 4; utf8 = true; continue; }
                utf8 = false; break;
            }
            if (utf8 == true) {
                text = Encoding.UTF8.GetString(b);
                return Encoding.UTF8;
            }


            // The next check is a heuristic attempt to detect UTF-16 without a BOM.
            // We simply look for zeroes in odd or even byte places, and if a certain
            // threshold is reached, the code is 'probably' UF-16.          
            double threshold = 0.1; // proportion of chars step 2 which must be zeroed to be diagnosed as utf-16. 0.1 = 10%
            int count = 0;
            for (int n = 0; n < taster; n += 2) if (b[n] == 0) count++;
            if (((double)count) / taster > threshold) { text = Encoding.BigEndianUnicode.GetString(b); return Encoding.BigEndianUnicode; }
            count = 0;
            for (int n = 1; n < taster; n += 2) if (b[n] == 0) count++;
            if (((double)count) / taster > threshold) { text = Encoding.Unicode.GetString(b); return Encoding.Unicode; } // (little-endian)


            // Finally, a long shot - let's see if we can find "charset=xyz" or
            // "encoding=xyz" to identify the encoding:
            for (int n = 0; n < taster-9; n++)
            {
                if (
                    ((b[n + 0] == 'c' || b[n + 0] == 'C') && (b[n + 1] == 'h' || b[n + 1] == 'H') && (b[n + 2] == 'a' || b[n + 2] == 'A') && (b[n + 3] == 'r' || b[n + 3] == 'R') && (b[n + 4] == 's' || b[n + 4] == 'S') && (b[n + 5] == 'e' || b[n + 5] == 'E') && (b[n + 6] == 't' || b[n + 6] == 'T') && (b[n + 7] == '=')) ||
                    ((b[n + 0] == 'e' || b[n + 0] == 'E') && (b[n + 1] == 'n' || b[n + 1] == 'N') && (b[n + 2] == 'c' || b[n + 2] == 'C') && (b[n + 3] == 'o' || b[n + 3] == 'O') && (b[n + 4] == 'd' || b[n + 4] == 'D') && (b[n + 5] == 'i' || b[n + 5] == 'I') && (b[n + 6] == 'n' || b[n + 6] == 'N') && (b[n + 7] == 'g' || b[n + 7] == 'G') && (b[n + 8] == '='))
                    )
                {
                    if (b[n + 0] == 'c' || b[n + 0] == 'C') n += 8; else n += 9;
                    if (b[n] == '"' || b[n] == '\'') n++;
                    int oldn = n;
                    while (n < taster && (b[n] == '_' || b[n] == '-' || (b[n] >= '0' && b[n] <= '9') || (b[n] >= 'a' && b[n] <= 'z') || (b[n] >= 'A' && b[n] <= 'Z')))
                    { n++; }
                    byte[] nb = new byte[n-oldn];
                    Array.Copy(b, oldn, nb, 0, n-oldn);
                    try {
                        string internalEnc = Encoding.ASCII.GetString(nb);
                        text = Encoding.GetEncoding(internalEnc).GetString(b);
                        return Encoding.GetEncoding(internalEnc);
                    }
                    catch { break; }    // If C# doesn't recognize the name of the encoding, break.
                }
            }


            // If all else fails, the encoding is probably (though certainly not
            // definitely) the user's local codepage! One might present to the user a
            // list of alternative encodings as shown here: http://stackoverflow.com/questions/8509339/what-is-the-most-common-encoding-of-each-language
            // A full list can be found using Encoding.GetEncodings();
            text = Encoding.Default.GetString(b);
            return Encoding.Default;
        }

        public static bool IsStandartEncoding(string text)
        {
            var encod = DetectTextEncoding(text);
            return encod == Encoding.Default || encod == Encoding.ASCII || encod == Encoding.UTF7 || encod == Encoding.UTF8 || encod == Encoding.Unicode || encod == Encoding.UTF32;
        }

        ///<summary>
        /// Loop over all available encodings and store those
        /// with a preamble in the _preambles list.
        /// The list is sorted by preamble length,
        /// longest preamble first. This prevents
        /// a short preamble 'masking' a longer one
        /// later in the list.
        ///</summary>
        protected static void ScanEncodings()
        {
            // Create a new sorted list of preambles
            _preambles = new SortedList<int, PreambleInfo>();

            // Loop over all encodings
            foreach (EncodingInfo encodingInfo in Encoding.GetEncodings())
            {
                // Do we have a preamble?
                byte[] preamble = encodingInfo.GetEncoding().GetPreamble();
                if (preamble.Length > 0)
                {
                    // Add it to the collection, inversely sorted by preamble length
                    // (and code page, to keep the keys unique)
                    _preambles.Add(-(preamble.Length * 1000000 + encodingInfo.CodePage),
                       new PreambleInfo(encodingInfo.GetEncoding(), preamble));

                    // Update the maximum preamble length if this one's longer
                    if (preamble.Length > _maxPreambleLength)
                    {
                        _maxPreambleLength = preamble.Length;
                    }
                }
            }
        }
    }
}
