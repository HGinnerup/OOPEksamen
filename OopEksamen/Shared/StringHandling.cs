using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public static class StringHandling
    {
        public static IEnumerable<string> SplitString(string str, char delimiter)
        {
            var currentString = string.Empty;
            var isInQuotation = false;

            for (var i = 0; true; i++)
            {
                if(i == str.Length)
                {
                    if (isInQuotation) throw new Exception("Expected quotationmark");
                    yield return currentString;
                    break;
                }
                // Quotation mark found, either mark field as quoted, or expect field-end
                if (str[i] == '"')
                {
                    if (currentString.Length == 0)
                    {
                        isInQuotation = true;
                    }
                    else if (isInQuotation)
                    {
                        i++;
                        if (str.Length == i || str[i] == delimiter)
                        {
                            yield return currentString;
                            currentString = string.Empty;
                            isInQuotation = false;
                            if (str.Length == i) break;
                        }
                        else
                        {
                            throw new Exception("Unexpected quotationmark");
                        }
                    }
                    else if (!isInQuotation)
                    {
                        currentString += str[i];
                    }
                }

                // Delimiter found, ignore if field is quoted
                else if (str[i] == delimiter)
                {
                    if (isInQuotation) currentString += str[i];
                    else
                    {
                        yield return currentString;
                        currentString = string.Empty;
                        isInQuotation = false;
                    }
                }

                // Escape sequence
                else if (str[i] == '\\')
                {
                    i++;
                    switch (str[i])
                    {
                        case '"':
                        case '\\':
                        case ' ':
                            currentString += str[i];
                            break;
                        default:
                            if (str[i] == delimiter)
                            {
                                currentString += str[i];
                            }
                            else
                            {
                                throw new Exception($@"Unknown escape-token \{str[i]}");
                            }
                            break;
                    }
                }
                else
                {
                    currentString += str[i];
                }
            }
            yield break;
        }
    }
}
