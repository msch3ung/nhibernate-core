﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using NHibernate.Criterion;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1324
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		public override string BugNumber
		{
			get { return "NH1324"; }
		}


		[Test]
		public async Task CanUseUniqueResultWithNullableType_ReturnNull_CriteriaAsync()
		{
			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				Person p = new Person("a", null, 4);
				await (s.SaveAsync(p));
				await (tx.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				int? result = await (s.CreateCriteria(typeof (Person))
					.SetProjection(Projections.Property("IQ"))
					.UniqueResultAsync<int?>());
				Assert.IsNull(result);
				await (tx.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				await (s.DeleteAsync("from Person"));
				await (tx.CommitAsync());
			}
		}

		[Test]
		public async Task CanUseUniqueResultWithNullableType_ReturnResult_CriteriaAsync()
		{
			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				Person p = new Person("a", 4, 4);
				await (s.SaveAsync(p));
				await (tx.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				int? result = await (s.CreateCriteria(typeof(Person))
					.SetProjection(Projections.Property("IQ"))
					.UniqueResultAsync<int?>());
				Assert.AreEqual(4, result);
				await (tx.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				await (s.DeleteAsync("from Person"));
				await (tx.CommitAsync());
			}
		}

		[Test]
		public async Task CanUseUniqueResultWithNullableType_ReturnNull_HQLAsync()
		{
			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				Person p = new Person("a", null, 4);
				await (s.SaveAsync(p));
				await (tx.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				int? result = await (s.CreateQuery("select p.IQ from Person p")
					.UniqueResultAsync<int?>());
				Assert.IsNull(result);
				await (tx.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				await (s.DeleteAsync("from Person"));
				await (tx.CommitAsync());
			}
		}

		[Test]
		public async Task CanUseUniqueResultWithNullableType_ReturnResult_HQLAsync()
		{
			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				Person p = new Person("a", 4, 4);
				await (s.SaveAsync(p));
				await (tx.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				int? result = await (s.CreateQuery("select p.IQ from Person p")
					.UniqueResultAsync<int?>());
				Assert.AreEqual(4, result);
				await (tx.CommitAsync());
			}

			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				await (s.DeleteAsync("from Person"));
				await (tx.CommitAsync());
			}
		}
	}
}