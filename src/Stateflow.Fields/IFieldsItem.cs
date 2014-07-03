namespace Stateflow.Fields
{

    public interface IFieldsItem<TIdentifier> : IRevision<TIdentifier>, IIdentifiableBy<TIdentifier>
    {

        bool IsDirty { get; }
        bool IsNew { get; }

        /// <summary>
        /// Returns the revision number of this item.
        /// </summary>
        int RevisionNumber { get; }

        RevisionCollection<TIdentifier> Revisions { get; }
        object GetFieldValue(TIdentifier id, int revision);

        FieldData<TIdentifier> FieldData { get; }

        /// <summary>
        /// Sets or gets the value of a field directly using the identifier of the field.
        /// </summary>
        /// <param name="id">The identifier of the field to access.</param>
        /// <returns>The value of the field.</returns>
        object this[TIdentifier id] { get; set; }

        /// <summary>
        /// Sets or gets the value of a field directly using the name of the field.
        /// </summary>
        /// <param name="name">The name of the field to access.</param>
        /// <returns>The value of the field.</returns>
        object this[string name] { get; set; }

        /// <summary>
        /// Save the current item to the store.
        /// </summary>
        void Save();
        bool Validate();
    }

}
