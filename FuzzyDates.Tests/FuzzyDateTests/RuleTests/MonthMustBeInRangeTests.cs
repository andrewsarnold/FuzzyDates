﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FuzzyDates.Tests.RuleTests.FuzzyDateTests
{
	[TestClass]
	public class MonthMustBeInRangeTests
	{
		[TestMethod]
		public void MonthLessThanOneIsInvalid()
		{
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => _ = new FuzzyDate(2018, 0));
		}

		[TestMethod]
		public void MonthGreaterThanTwelveIsInvalid()
		{
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => _ = new FuzzyDate(2018, 13));
		}

		[TestMethod]
		public void MonthInRangeIsValid()
		{
			try
			{
				_ = new FuzzyDate(2019, 9);
			}
			catch (Exception ex)
			{
				Assert.Fail($"Expect no exception, but got {ex.Message}");
			}
		}
	}
}
