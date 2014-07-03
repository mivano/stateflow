using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Stateflow.Fields.DataStores
{

    public interface IDataStore<TIdentifier>
    {

        /// <summary>
        /// Loads a template given its identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        ITemplate<TIdentifier> LoadTemplate(TIdentifier identifier);
        /// <summary>
        /// Saves a template.
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        TIdentifier SaveTemplate(ITemplate<TIdentifier> template);
        /// <summary>
        /// Removes a template.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        TIdentifier RemoveTemplate(TIdentifier identifier);
        /// <summary>
        /// Query all the templates.
        /// </summary>
        /// <returns></returns>
        IQueryable<ITemplate<TIdentifier>> GetAllTemplates();

        /// <summary>
        /// Loads a field item from the store.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        IFieldsItem<TIdentifier> LoadItem(TIdentifier identifier);

        /// <summary>
        /// Saves a field item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        TIdentifier SaveItem(IFieldsItem<TIdentifier> item);

        /// <summary>
        /// Returns all field items.
        /// </summary>
        /// <returns></returns>
        IQueryable<IFieldsItem<TIdentifier>> GetAllItems();

        /// <summary>
        /// Removes a single item.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        TIdentifier RemoveItem(TIdentifier identifier);
    }

}
