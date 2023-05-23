using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests
{
    internal static class TestHelper
    {
    
        /// <summary>
        /// Returns enumerable of test cases with human readable tests description from data
        /// </summary>
        /// <param name="objects">Test data</param>
        /// <returns>Enumerable of TestCaseData with formatted names</returns>
        public static IEnumerable ToTestCases<T>(this IEnumerable<T> objects)
        {
            return objects.Select(d =>
            {
                var testCaseData = new TestCaseData(d);
                testCaseData.SetName(d.ToString());
                return testCaseData;
            });
        }

    }
}