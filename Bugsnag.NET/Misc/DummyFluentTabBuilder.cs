using Bugsnag.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.NET.Misc
{
    public class DummyFluentTabBuilder : IFluentTabBuilder
    {
        public static IFluentTabBuilder CreateWithCallback(Exception exception, string tabName, Action<IFluentTabBuilder> action)
        {
            var builder = new DummyFluentTabBuilder(exception, tabName);

            action(builder);

            return builder;
        }

        public DummyFluentTabBuilder(Exception exception, string tabName)
        {
            _exception = exception;
            _tabName = tabName;
        }

        readonly Exception _exception;
        readonly string _tabName;

        public IFluentTabBuilder Add(string value)
        {
            return this;
        }

        public IFluentTabBuilder Add(string key, string value)
        {
            return this;
        }

        public IFluentTabBuilder Add(string key, IEnumerable<string> values)
        {
            return this;
        }

        public IFluentTabBuilder Add(string key, object value)
        {
            return this;
        }

        public IFluentTabBuilder Tab(string tabName) =>
            new DummyFluentTabBuilder(
                exception: _exception,
                tabName: tabName);

        public IFluentTabBuilder Tab(string tabName, Action<IFluentTabBuilder> action) =>
            CreateWithCallback(
                exception: _exception,
                tabName: tabName,
                action: action);
    }

    public static class DummyFluentTabBuilderExtensions
    {
        public static IFluentTabBuilder Tab(this Exception exception, string tabName) =>
            new DummyFluentTabBuilder(
                exception: exception,
                tabName: tabName);

        public static IFluentTabBuilder Tab(this Exception exception, string tabName, Action<IFluentTabBuilder> action) =>
            DummyFluentTabBuilder.CreateWithCallback(
                exception: exception,
                tabName: tabName,
                action: action);
    }
}
