﻿using System;
using System.Runtime.Serialization;
using FuzzyDates.Rules;

namespace FuzzyDates
{
	public class FuzzyDateRange : IComparable<FuzzyDateRange>, ISerializable
	{
		/// <summary>
		/// Gets the "from" component of the range represented by this instance.
		/// </summary>
		public FuzzyDate From { get; }

		/// <summary>
		/// Gets the "to" component of the range represented by this instance.
		/// </summary>
		public FuzzyDate To { get; }

		/// <summary>
		/// Initializes a new instance of the FuzzyDateRange class with the specified From and To values.
		/// </summary>
		/// <param name="from">The From value. Can be any FuzzyDate.</param>
		/// <param name="to">The To value. Can be any FuzzyDate.</param>
		public FuzzyDateRange(FuzzyDate from, FuzzyDate to)
		{
			From = from ?? FuzzyDate.Unknown;
			To = to ?? FuzzyDate.Unknown;

			RulesRunner.RunRules(this);
		}

		/// <summary>
		/// For testing purposes only. You should be using
		/// some sort of display adapter to control how
		/// dates are displayed to a consumer.
		/// </summary>
		/// <returns>A human-readable string representation of the date range.</returns>
		public override string ToString()
		{
			return $"{From}-{To}";
		}

		/// <summary>
		/// Converts a FuzzyDateRange to a .NET TimeSpan object.
		/// </summary>
		/// <returns>A TimeSpan based on the DateTime conversions of From and To.</returns>
		public TimeSpan ToTimeSpan()
		{
			var from = From.ToDateTime();
			var to = To.ToDateTime();
			return to - from;
		}

		/// <summary>
		/// Populates a SerializationInfo object with the data needed to serialize the current FuzzyDate object.
		/// </summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify null.)</param>
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException(nameof(info));
			}

			info.AddValue("From", From);
			info.AddValue("To", To);
		}

		/// <summary>
		/// Compares the value of this instance to a specified FuzzyDateRange value and returns an integer
		/// that indicates whether this instance is earlier than, the same as, or later than the
		/// specified FuzzyDateRange value.
		/// </summary>
		/// <param name="value">The object to compare to the current instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and the value parameter.</returns>
		public int CompareTo(FuzzyDateRange value)
		{
			var fromCompare = From.CompareTo(value.From);
			if (fromCompare != 0)
			{
				return fromCompare;
			}

			return To.CompareTo(value.To);
		}
	}
}