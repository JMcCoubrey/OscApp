using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OscApp.DAL.Tests
{
    public abstract class MockableDbSetWithIQueryable<T> : DbSet<T>, IQueryable<T>
        where T : class
    {
        public abstract IEnumerator<T> GetEnumerator();
        public abstract Expression Expression { get; }
        public abstract Type ElementType { get; }
        public abstract IQueryProvider Provider { get; }


    }

    // Extension method to create Mockable db set from any IEnumerable
    public static class MockableDbSetExtensions
    {
        public static Mock<MockableDbSetWithIQueryable<T>> ToDbSet<T>(this IEnumerable<T> source) where T : class
        {
            // convert source data into queryable
            var data = source.AsQueryable();

            // create out mocked out set using generics
            var mockSet = new Mock<MockableDbSetWithIQueryable<T>>();

            // setup this mocked set to return the data in the queryable form
            mockSet.Setup(m => m.Provider).Returns(data.Provider);
            mockSet.Setup(m => m.Expression).Returns(data.Expression);
            mockSet.Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }
    }
}
