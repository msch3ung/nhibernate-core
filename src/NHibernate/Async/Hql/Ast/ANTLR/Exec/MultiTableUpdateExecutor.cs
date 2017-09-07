﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using NHibernate.Engine;
using NHibernate.Exceptions;
using NHibernate.Hql.Ast.ANTLR.Tree;
using NHibernate.Param;
using NHibernate.Persister.Entity;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
using NHibernate.Util;
using IQueryable = NHibernate.Persister.Entity.IQueryable;

namespace NHibernate.Hql.Ast.ANTLR.Exec
{
	using System.Threading.Tasks;
	using System.Threading;
	/// <content>
	/// Contains generated async methods
	/// </content>
	public partial class MultiTableUpdateExecutor : AbstractStatementExecutor
	{

		public override async Task<int> ExecuteAsync(QueryParameters parameters, ISessionImplementor session, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			await (CoordinateSharedCacheCleanupAsync(session, cancellationToken)).ConfigureAwait(false);

			await (CreateTemporaryTableIfNecessaryAsync(persister, session, cancellationToken)).ConfigureAwait(false);

			try
			{
				// First, save off the pertinent ids, as the return value
				DbCommand ps = null;
				int resultCount;
				try
				{
					try
					{
						int parameterStart = Walker.NumberOfParametersInSetClause;

						IList<IParameterSpecification> allParams = Walker.Parameters;

						List<IParameterSpecification> whereParams = (new List<IParameterSpecification>(allParams)).GetRange(
							parameterStart, allParams.Count - parameterStart);

						var sqlQueryParametersList = idInsertSelect.GetParameters().ToList();
						SqlType[] parameterTypes = whereParams.GetQueryParameterTypes(sqlQueryParametersList, session.Factory);

						ps = await (session.Batcher.PrepareCommandAsync(CommandType.Text, idInsertSelect, parameterTypes, cancellationToken)).ConfigureAwait(false);
						foreach (var parameterSpecification in whereParams)
						{
							await (parameterSpecification.BindAsync(ps, sqlQueryParametersList, parameters, session, cancellationToken)).ConfigureAwait(false);
						}

						resultCount = await (session.Batcher.ExecuteNonQueryAsync(ps, cancellationToken)).ConfigureAwait(false);
					}
					finally
					{
						if (ps != null)
						{
							session.Batcher.CloseCommand(ps, null);
						}
					}
				}
				catch (DbException e)
				{
					throw ADOExceptionHelper.Convert(Factory.SQLExceptionConverter, e, "could not insert/select ids for bulk update",
					                                 idInsertSelect);
				}

				// Start performing the updates
				for (int i = 0; i < updates.Length; i++)
				{
					if (updates[i] == null)
					{
						continue;
					}
					try
					{
						try
						{
							var sqlQueryParametersList = updates[i].GetParameters().ToList();
							var paramsSpec = hqlParameters[i];
							SqlType[] parameterTypes = paramsSpec.GetQueryParameterTypes(sqlQueryParametersList, session.Factory);

							ps = await (session.Batcher.PrepareCommandAsync(CommandType.Text, updates[i], parameterTypes, cancellationToken)).ConfigureAwait(false);
							foreach (var parameterSpecification in paramsSpec)
							{
								await (parameterSpecification.BindAsync(ps, sqlQueryParametersList, parameters, session, cancellationToken)).ConfigureAwait(false);
							}

							await (session.Batcher.ExecuteNonQueryAsync(ps, cancellationToken)).ConfigureAwait(false);
						}
						finally
						{
							if (ps != null)
							{
								session.Batcher.CloseCommand(ps, null);
							}
						}
					}
					catch (DbException e)
					{
						throw ADOExceptionHelper.Convert(Factory.SQLExceptionConverter, e, "error performing bulk update", updates[i]);
					}
				}

				return resultCount;
			}
			finally
			{
				await (DropTemporaryTableIfNecessaryAsync(persister, session, cancellationToken)).ConfigureAwait(false);
			}
		}
	}
}