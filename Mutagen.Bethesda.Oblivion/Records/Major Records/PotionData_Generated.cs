/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Autogenerated by Loqui.  Do not manually change.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
*/
#region Usings
using Loqui;
using Loqui.Internal;
using Mutagen.Bethesda.Binary;
using Mutagen.Bethesda.Internals;
using Mutagen.Bethesda.Oblivion.Internals;
using Noggog;
using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
#endregion

#nullable enable
namespace Mutagen.Bethesda.Oblivion
{
    #region Class
    public partial class PotionData :
        IPotionData,
        ILoquiObjectSetter<PotionData>,
        IEquatable<IPotionDataGetter>
    {
        #region Ctor
        public PotionData()
        {
            CustomCtor();
        }
        partial void CustomCtor();
        #endregion

        #region Value
        public UInt32 Value { get; set; } = default;
        #endregion
        #region Flags
        public IngredientFlag Flags { get; set; } = default;
        #endregion

        #region To String

        public void ToString(
            FileGeneration fg,
            string? name = null)
        {
            PotionDataMixIn.ToString(
                item: this,
                name: name);
        }

        #endregion

        #region Equals and Hash
        public override bool Equals(object? obj)
        {
            if (!(obj is IPotionDataGetter rhs)) return false;
            return ((PotionDataCommon)((IPotionDataGetter)this).CommonInstance()!).Equals(this, rhs);
        }

        public bool Equals(IPotionDataGetter? obj)
        {
            return ((PotionDataCommon)((IPotionDataGetter)this).CommonInstance()!).Equals(this, obj);
        }

        public override int GetHashCode() => ((PotionDataCommon)((IPotionDataGetter)this).CommonInstance()!).GetHashCode(this);

        #endregion

        #region Mask
        public class Mask<TItem> :
            IMask<TItem>,
            IEquatable<Mask<TItem>>
        {
            #region Ctors
            public Mask(TItem initialValue)
            {
                this.Value = initialValue;
                this.Flags = initialValue;
            }

            public Mask(
                TItem Value,
                TItem Flags)
            {
                this.Value = Value;
                this.Flags = Flags;
            }

            #pragma warning disable CS8618
            protected Mask()
            {
            }
            #pragma warning restore CS8618

            #endregion

            #region Members
            public TItem Value;
            public TItem Flags;
            #endregion

            #region Equals
            public override bool Equals(object? obj)
            {
                if (!(obj is Mask<TItem> rhs)) return false;
                return Equals(rhs);
            }

            public bool Equals(Mask<TItem>? rhs)
            {
                if (rhs == null) return false;
                if (!object.Equals(this.Value, rhs.Value)) return false;
                if (!object.Equals(this.Flags, rhs.Flags)) return false;
                return true;
            }
            public override int GetHashCode()
            {
                var hash = new HashCode();
                hash.Add(this.Value);
                hash.Add(this.Flags);
                return hash.ToHashCode();
            }

            #endregion

            #region All
            public bool All(Func<TItem, bool> eval)
            {
                if (!eval(this.Value)) return false;
                if (!eval(this.Flags)) return false;
                return true;
            }
            #endregion

            #region Any
            public bool Any(Func<TItem, bool> eval)
            {
                if (eval(this.Value)) return true;
                if (eval(this.Flags)) return true;
                return false;
            }
            #endregion

            #region Translate
            public Mask<R> Translate<R>(Func<TItem, R> eval)
            {
                var ret = new PotionData.Mask<R>();
                this.Translate_InternalFill(ret, eval);
                return ret;
            }

            protected void Translate_InternalFill<R>(Mask<R> obj, Func<TItem, R> eval)
            {
                obj.Value = eval(this.Value);
                obj.Flags = eval(this.Flags);
            }
            #endregion

            #region To String
            public override string ToString()
            {
                return ToString(printMask: null);
            }

            public string ToString(PotionData.Mask<bool>? printMask = null)
            {
                var fg = new FileGeneration();
                ToString(fg, printMask);
                return fg.ToString();
            }

            public void ToString(FileGeneration fg, PotionData.Mask<bool>? printMask = null)
            {
                fg.AppendLine($"{nameof(PotionData.Mask<TItem>)} =>");
                fg.AppendLine("[");
                using (new DepthWrapper(fg))
                {
                    if (printMask?.Value ?? true)
                    {
                        fg.AppendItem(Value, "Value");
                    }
                    if (printMask?.Flags ?? true)
                    {
                        fg.AppendItem(Flags, "Flags");
                    }
                }
                fg.AppendLine("]");
            }
            #endregion

        }

        public class ErrorMask :
            IErrorMask,
            IErrorMask<ErrorMask>
        {
            #region Members
            public Exception? Overall { get; set; }
            private List<string>? _warnings;
            public List<string> Warnings
            {
                get
                {
                    if (_warnings == null)
                    {
                        _warnings = new List<string>();
                    }
                    return _warnings;
                }
            }
            public Exception? Value;
            public Exception? Flags;
            #endregion

            #region IErrorMask
            public object? GetNthMask(int index)
            {
                PotionData_FieldIndex enu = (PotionData_FieldIndex)index;
                switch (enu)
                {
                    case PotionData_FieldIndex.Value:
                        return Value;
                    case PotionData_FieldIndex.Flags:
                        return Flags;
                    default:
                        throw new ArgumentException($"Index is out of range: {index}");
                }
            }

            public void SetNthException(int index, Exception ex)
            {
                PotionData_FieldIndex enu = (PotionData_FieldIndex)index;
                switch (enu)
                {
                    case PotionData_FieldIndex.Value:
                        this.Value = ex;
                        break;
                    case PotionData_FieldIndex.Flags:
                        this.Flags = ex;
                        break;
                    default:
                        throw new ArgumentException($"Index is out of range: {index}");
                }
            }

            public void SetNthMask(int index, object obj)
            {
                PotionData_FieldIndex enu = (PotionData_FieldIndex)index;
                switch (enu)
                {
                    case PotionData_FieldIndex.Value:
                        this.Value = (Exception?)obj;
                        break;
                    case PotionData_FieldIndex.Flags:
                        this.Flags = (Exception?)obj;
                        break;
                    default:
                        throw new ArgumentException($"Index is out of range: {index}");
                }
            }

            public bool IsInError()
            {
                if (Overall != null) return true;
                if (Value != null) return true;
                if (Flags != null) return true;
                return false;
            }
            #endregion

            #region To String
            public override string ToString()
            {
                var fg = new FileGeneration();
                ToString(fg, null);
                return fg.ToString();
            }

            public void ToString(FileGeneration fg, string? name = null)
            {
                fg.AppendLine($"{(name ?? "ErrorMask")} =>");
                fg.AppendLine("[");
                using (new DepthWrapper(fg))
                {
                    if (this.Overall != null)
                    {
                        fg.AppendLine("Overall =>");
                        fg.AppendLine("[");
                        using (new DepthWrapper(fg))
                        {
                            fg.AppendLine($"{this.Overall}");
                        }
                        fg.AppendLine("]");
                    }
                    ToString_FillInternal(fg);
                }
                fg.AppendLine("]");
            }
            protected void ToString_FillInternal(FileGeneration fg)
            {
                fg.AppendItem(Value, "Value");
                fg.AppendItem(Flags, "Flags");
            }
            #endregion

            #region Combine
            public ErrorMask Combine(ErrorMask? rhs)
            {
                if (rhs == null) return this;
                var ret = new ErrorMask();
                ret.Value = this.Value.Combine(rhs.Value);
                ret.Flags = this.Flags.Combine(rhs.Flags);
                return ret;
            }
            public static ErrorMask? Combine(ErrorMask? lhs, ErrorMask? rhs)
            {
                if (lhs != null && rhs != null) return lhs.Combine(rhs);
                return lhs ?? rhs;
            }
            #endregion

            #region Factory
            public static ErrorMask Factory(ErrorMaskBuilder errorMask)
            {
                return new ErrorMask();
            }
            #endregion

        }
        public class TranslationMask : ITranslationMask
        {
            #region Members
            private TranslationCrystal? _crystal;
            public readonly bool DefaultOn;
            public bool Value;
            public bool Flags;
            #endregion

            #region Ctors
            public TranslationMask(bool defaultOn)
            {
                this.DefaultOn = defaultOn;
                this.Value = defaultOn;
                this.Flags = defaultOn;
            }

            #endregion

            public TranslationCrystal GetCrystal()
            {
                if (_crystal != null) return _crystal;
                var ret = new List<(bool On, TranslationCrystal? SubCrystal)>();
                GetCrystal(ret);
                _crystal = new TranslationCrystal(ret.ToArray());
                return _crystal;
            }

            protected void GetCrystal(List<(bool On, TranslationCrystal? SubCrystal)> ret)
            {
                ret.Add((Value, null));
                ret.Add((Flags, null));
            }

            public static implicit operator TranslationMask(bool defaultOn)
            {
                return new TranslationMask(defaultOn);
            }

        }
        #endregion

        #region Mutagen
        public static readonly RecordType GrupRecordType = PotionData_Registration.TriggeringRecordType;
        #endregion

        #region Binary Translation
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected object BinaryWriteTranslator => PotionDataBinaryWriteTranslation.Instance;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        object IBinaryItem.BinaryWriteTranslator => this.BinaryWriteTranslator;
        void IBinaryItem.WriteToBinary(
            MutagenWriter writer,
            RecordTypeConverter? recordTypeConverter = null)
        {
            ((PotionDataBinaryWriteTranslation)this.BinaryWriteTranslator).Write(
                item: this,
                writer: writer,
                recordTypeConverter: recordTypeConverter);
        }
        #region Binary Create
        public static PotionData CreateFromBinary(
            MutagenFrame frame,
            RecordTypeConverter? recordTypeConverter = null)
        {
            var ret = new PotionData();
            ((PotionDataSetterCommon)((IPotionDataGetter)ret).CommonSetterInstance()!).CopyInFromBinary(
                item: ret,
                frame: frame,
                recordTypeConverter: recordTypeConverter);
            return ret;
        }

        #endregion

        public static bool TryCreateFromBinary(
            MutagenFrame frame,
            out PotionData item,
            RecordTypeConverter? recordTypeConverter = null)
        {
            var startPos = frame.Position;
            item = CreateFromBinary(frame, recordTypeConverter);
            return startPos != frame.Position;
        }
        #endregion

        void IPrintable.ToString(FileGeneration fg, string? name) => this.ToString(fg, name);

        void IClearable.Clear()
        {
            ((PotionDataSetterCommon)((IPotionDataGetter)this).CommonSetterInstance()!).Clear(this);
        }

        internal static PotionData GetNew()
        {
            return new PotionData();
        }

    }
    #endregion

    #region Interface
    public partial interface IPotionData :
        IPotionDataGetter,
        ILoquiObjectSetter<IPotionData>
    {
        new UInt32 Value { get; set; }
        new IngredientFlag Flags { get; set; }
    }

    public partial interface IPotionDataGetter :
        ILoquiObject,
        ILoquiObject<IPotionDataGetter>,
        IBinaryItem
    {
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        object CommonInstance();
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        object? CommonSetterInstance();
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        object CommonSetterTranslationInstance();
        static ILoquiRegistration Registration => PotionData_Registration.Instance;
        UInt32 Value { get; }
        IngredientFlag Flags { get; }

    }

    #endregion

    #region Common MixIn
    public static partial class PotionDataMixIn
    {
        public static void Clear(this IPotionData item)
        {
            ((PotionDataSetterCommon)((IPotionDataGetter)item).CommonSetterInstance()!).Clear(item: item);
        }

        public static PotionData.Mask<bool> GetEqualsMask(
            this IPotionDataGetter item,
            IPotionDataGetter rhs,
            EqualsMaskHelper.Include include = EqualsMaskHelper.Include.All)
        {
            return ((PotionDataCommon)((IPotionDataGetter)item).CommonInstance()!).GetEqualsMask(
                item: item,
                rhs: rhs,
                include: include);
        }

        public static string ToString(
            this IPotionDataGetter item,
            string? name = null,
            PotionData.Mask<bool>? printMask = null)
        {
            return ((PotionDataCommon)((IPotionDataGetter)item).CommonInstance()!).ToString(
                item: item,
                name: name,
                printMask: printMask);
        }

        public static void ToString(
            this IPotionDataGetter item,
            FileGeneration fg,
            string? name = null,
            PotionData.Mask<bool>? printMask = null)
        {
            ((PotionDataCommon)((IPotionDataGetter)item).CommonInstance()!).ToString(
                item: item,
                fg: fg,
                name: name,
                printMask: printMask);
        }

        public static bool Equals(
            this IPotionDataGetter item,
            IPotionDataGetter rhs)
        {
            return ((PotionDataCommon)((IPotionDataGetter)item).CommonInstance()!).Equals(
                lhs: item,
                rhs: rhs);
        }

        public static void DeepCopyIn(
            this IPotionData lhs,
            IPotionDataGetter rhs)
        {
            ((PotionDataSetterTranslationCommon)((IPotionDataGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: default,
                copyMask: default,
                deepCopy: false);
        }

        public static void DeepCopyIn(
            this IPotionData lhs,
            IPotionDataGetter rhs,
            PotionData.TranslationMask? copyMask = null)
        {
            ((PotionDataSetterTranslationCommon)((IPotionDataGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: default,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: false);
        }

        public static void DeepCopyIn(
            this IPotionData lhs,
            IPotionDataGetter rhs,
            out PotionData.ErrorMask errorMask,
            PotionData.TranslationMask? copyMask = null)
        {
            var errorMaskBuilder = new ErrorMaskBuilder();
            ((PotionDataSetterTranslationCommon)((IPotionDataGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: errorMaskBuilder,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: false);
            errorMask = PotionData.ErrorMask.Factory(errorMaskBuilder);
        }

        public static void DeepCopyIn(
            this IPotionData lhs,
            IPotionDataGetter rhs,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask)
        {
            ((PotionDataSetterTranslationCommon)((IPotionDataGetter)lhs).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: lhs,
                rhs: rhs,
                errorMask: errorMask,
                copyMask: copyMask,
                deepCopy: false);
        }

        public static PotionData DeepCopy(
            this IPotionDataGetter item,
            PotionData.TranslationMask? copyMask = null)
        {
            return ((PotionDataSetterTranslationCommon)((IPotionDataGetter)item).CommonSetterTranslationInstance()!).DeepCopy(
                item: item,
                copyMask: copyMask);
        }

        public static PotionData DeepCopy(
            this IPotionDataGetter item,
            out PotionData.ErrorMask errorMask,
            PotionData.TranslationMask? copyMask = null)
        {
            return ((PotionDataSetterTranslationCommon)((IPotionDataGetter)item).CommonSetterTranslationInstance()!).DeepCopy(
                item: item,
                copyMask: copyMask,
                errorMask: out errorMask);
        }

        public static PotionData DeepCopy(
            this IPotionDataGetter item,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask = null)
        {
            return ((PotionDataSetterTranslationCommon)((IPotionDataGetter)item).CommonSetterTranslationInstance()!).DeepCopy(
                item: item,
                copyMask: copyMask,
                errorMask: errorMask);
        }

        #region Binary Translation
        public static void CopyInFromBinary(
            this IPotionData item,
            MutagenFrame frame,
            RecordTypeConverter? recordTypeConverter = null)
        {
            ((PotionDataSetterCommon)((IPotionDataGetter)item).CommonSetterInstance()!).CopyInFromBinary(
                item: item,
                frame: frame,
                recordTypeConverter: recordTypeConverter);
        }

        #endregion

    }
    #endregion

}

namespace Mutagen.Bethesda.Oblivion.Internals
{
    #region Field Index
    public enum PotionData_FieldIndex
    {
        Value = 0,
        Flags = 1,
    }
    #endregion

    #region Registration
    public partial class PotionData_Registration : ILoquiRegistration
    {
        public static readonly PotionData_Registration Instance = new PotionData_Registration();

        public static ProtocolKey ProtocolKey => ProtocolDefinition_Oblivion.ProtocolKey;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_Oblivion.ProtocolKey,
            msgID: 202,
            version: 0);

        public const string GUID = "bbd59739-d2a2-4702-93dc-71ffd5a4310e";

        public const ushort AdditionalFieldCount = 2;

        public const ushort FieldCount = 2;

        public static readonly Type MaskType = typeof(PotionData.Mask<>);

        public static readonly Type ErrorMaskType = typeof(PotionData.ErrorMask);

        public static readonly Type ClassType = typeof(PotionData);

        public static readonly Type GetterType = typeof(IPotionDataGetter);

        public static readonly Type? InternalGetterType = null;

        public static readonly Type SetterType = typeof(IPotionData);

        public static readonly Type? InternalSetterType = null;

        public const string FullName = "Mutagen.Bethesda.Oblivion.PotionData";

        public const string Name = "PotionData";

        public const string Namespace = "Mutagen.Bethesda.Oblivion";

        public const byte GenericCount = 0;

        public static readonly Type? GenericRegistrationType = null;

        public static readonly RecordType TriggeringRecordType = RecordTypes.ENIT;
        public static readonly Type BinaryWriteTranslation = typeof(PotionDataBinaryWriteTranslation);
        #region Interface
        ProtocolKey ILoquiRegistration.ProtocolKey => ProtocolKey;
        ObjectKey ILoquiRegistration.ObjectKey => ObjectKey;
        string ILoquiRegistration.GUID => GUID;
        ushort ILoquiRegistration.FieldCount => FieldCount;
        ushort ILoquiRegistration.AdditionalFieldCount => AdditionalFieldCount;
        Type ILoquiRegistration.MaskType => MaskType;
        Type ILoquiRegistration.ErrorMaskType => ErrorMaskType;
        Type ILoquiRegistration.ClassType => ClassType;
        Type ILoquiRegistration.SetterType => SetterType;
        Type? ILoquiRegistration.InternalSetterType => InternalSetterType;
        Type ILoquiRegistration.GetterType => GetterType;
        Type? ILoquiRegistration.InternalGetterType => InternalGetterType;
        string ILoquiRegistration.FullName => FullName;
        string ILoquiRegistration.Name => Name;
        string ILoquiRegistration.Namespace => Namespace;
        byte ILoquiRegistration.GenericCount => GenericCount;
        Type? ILoquiRegistration.GenericRegistrationType => GenericRegistrationType;
        ushort? ILoquiRegistration.GetNameIndex(StringCaseAgnostic name) => throw new NotImplementedException();
        bool ILoquiRegistration.GetNthIsEnumerable(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.GetNthIsLoqui(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.GetNthIsSingleton(ushort index) => throw new NotImplementedException();
        string ILoquiRegistration.GetNthName(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.IsNthDerivative(ushort index) => throw new NotImplementedException();
        bool ILoquiRegistration.IsProtected(ushort index) => throw new NotImplementedException();
        Type ILoquiRegistration.GetNthType(ushort index) => throw new NotImplementedException();
        #endregion

    }
    #endregion

    #region Common
    public partial class PotionDataSetterCommon
    {
        public static readonly PotionDataSetterCommon Instance = new PotionDataSetterCommon();

        partial void ClearPartial();
        
        public void Clear(IPotionData item)
        {
            ClearPartial();
            item.Value = default;
            item.Flags = default;
        }
        
        #region Binary Translation
        public virtual void CopyInFromBinary(
            IPotionData item,
            MutagenFrame frame,
            RecordTypeConverter? recordTypeConverter = null)
        {
            frame = frame.SpawnWithFinalPosition(HeaderTranslation.ParseSubrecord(
                frame.Reader,
                recordTypeConverter.ConvertToCustom(RecordTypes.ENIT)));
            UtilityTranslation.SubrecordParse(
                record: item,
                frame: frame,
                recordTypeConverter: recordTypeConverter,
                fillStructs: PotionDataBinaryCreateTranslation.FillBinaryStructs);
        }
        
        #endregion
        
    }
    public partial class PotionDataCommon
    {
        public static readonly PotionDataCommon Instance = new PotionDataCommon();

        public PotionData.Mask<bool> GetEqualsMask(
            IPotionDataGetter item,
            IPotionDataGetter rhs,
            EqualsMaskHelper.Include include = EqualsMaskHelper.Include.All)
        {
            var ret = new PotionData.Mask<bool>(false);
            ((PotionDataCommon)((IPotionDataGetter)item).CommonInstance()!).FillEqualsMask(
                item: item,
                rhs: rhs,
                ret: ret,
                include: include);
            return ret;
        }
        
        public void FillEqualsMask(
            IPotionDataGetter item,
            IPotionDataGetter rhs,
            PotionData.Mask<bool> ret,
            EqualsMaskHelper.Include include = EqualsMaskHelper.Include.All)
        {
            if (rhs == null) return;
            ret.Value = item.Value == rhs.Value;
            ret.Flags = item.Flags == rhs.Flags;
        }
        
        public string ToString(
            IPotionDataGetter item,
            string? name = null,
            PotionData.Mask<bool>? printMask = null)
        {
            var fg = new FileGeneration();
            ToString(
                item: item,
                fg: fg,
                name: name,
                printMask: printMask);
            return fg.ToString();
        }
        
        public void ToString(
            IPotionDataGetter item,
            FileGeneration fg,
            string? name = null,
            PotionData.Mask<bool>? printMask = null)
        {
            if (name == null)
            {
                fg.AppendLine($"PotionData =>");
            }
            else
            {
                fg.AppendLine($"{name} (PotionData) =>");
            }
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
                ToStringFields(
                    item: item,
                    fg: fg,
                    printMask: printMask);
            }
            fg.AppendLine("]");
        }
        
        protected static void ToStringFields(
            IPotionDataGetter item,
            FileGeneration fg,
            PotionData.Mask<bool>? printMask = null)
        {
            if (printMask?.Value ?? true)
            {
                fg.AppendItem(item.Value, "Value");
            }
            if (printMask?.Flags ?? true)
            {
                fg.AppendItem(item.Flags, "Flags");
            }
        }
        
        #region Equals and Hash
        public virtual bool Equals(
            IPotionDataGetter? lhs,
            IPotionDataGetter? rhs)
        {
            if (lhs == null && rhs == null) return false;
            if (lhs == null || rhs == null) return false;
            if (lhs.Value != rhs.Value) return false;
            if (lhs.Flags != rhs.Flags) return false;
            return true;
        }
        
        public virtual int GetHashCode(IPotionDataGetter item)
        {
            var hash = new HashCode();
            hash.Add(item.Value);
            hash.Add(item.Flags);
            return hash.ToHashCode();
        }
        
        #endregion
        
        
        public object GetNew()
        {
            return PotionData.GetNew();
        }
        
        #region Mutagen
        public IEnumerable<FormKey> GetLinkFormKeys(IPotionDataGetter obj)
        {
            yield break;
        }
        
        public void RemapLinks(IPotionDataGetter obj, IReadOnlyDictionary<FormKey, FormKey> mapping) => throw new NotImplementedException();
        #endregion
        
    }
    public partial class PotionDataSetterTranslationCommon
    {
        public static readonly PotionDataSetterTranslationCommon Instance = new PotionDataSetterTranslationCommon();

        #region DeepCopyIn
        public void DeepCopyIn(
            IPotionData item,
            IPotionDataGetter rhs,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask,
            bool deepCopy)
        {
            if ((copyMask?.GetShouldTranslate((int)PotionData_FieldIndex.Value) ?? true))
            {
                item.Value = rhs.Value;
            }
            if ((copyMask?.GetShouldTranslate((int)PotionData_FieldIndex.Flags) ?? true))
            {
                item.Flags = rhs.Flags;
            }
        }
        
        #endregion
        
        public PotionData DeepCopy(
            IPotionDataGetter item,
            PotionData.TranslationMask? copyMask = null)
        {
            PotionData ret = (PotionData)((PotionDataCommon)((IPotionDataGetter)item).CommonInstance()!).GetNew();
            ((PotionDataSetterTranslationCommon)((IPotionDataGetter)ret).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: ret,
                rhs: item,
                errorMask: null,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: true);
            return ret;
        }
        
        public PotionData DeepCopy(
            IPotionDataGetter item,
            out PotionData.ErrorMask errorMask,
            PotionData.TranslationMask? copyMask = null)
        {
            var errorMaskBuilder = new ErrorMaskBuilder();
            PotionData ret = (PotionData)((PotionDataCommon)((IPotionDataGetter)item).CommonInstance()!).GetNew();
            ((PotionDataSetterTranslationCommon)((IPotionDataGetter)ret).CommonSetterTranslationInstance()!).DeepCopyIn(
                ret,
                item,
                errorMask: errorMaskBuilder,
                copyMask: copyMask?.GetCrystal(),
                deepCopy: true);
            errorMask = PotionData.ErrorMask.Factory(errorMaskBuilder);
            return ret;
        }
        
        public PotionData DeepCopy(
            IPotionDataGetter item,
            ErrorMaskBuilder? errorMask,
            TranslationCrystal? copyMask = null)
        {
            PotionData ret = (PotionData)((PotionDataCommon)((IPotionDataGetter)item).CommonInstance()!).GetNew();
            ((PotionDataSetterTranslationCommon)((IPotionDataGetter)ret).CommonSetterTranslationInstance()!).DeepCopyIn(
                item: ret,
                rhs: item,
                errorMask: errorMask,
                copyMask: copyMask,
                deepCopy: true);
            return ret;
        }
        
    }
    #endregion

}

namespace Mutagen.Bethesda.Oblivion
{
    public partial class PotionData
    {
        #region Common Routing
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILoquiRegistration ILoquiObject.Registration => PotionData_Registration.Instance;
        public static PotionData_Registration Registration => PotionData_Registration.Instance;
        [DebuggerStepThrough]
        protected object CommonInstance() => PotionDataCommon.Instance;
        [DebuggerStepThrough]
        protected object CommonSetterInstance()
        {
            return PotionDataSetterCommon.Instance;
        }
        [DebuggerStepThrough]
        protected object CommonSetterTranslationInstance() => PotionDataSetterTranslationCommon.Instance;
        [DebuggerStepThrough]
        object IPotionDataGetter.CommonInstance() => this.CommonInstance();
        [DebuggerStepThrough]
        object IPotionDataGetter.CommonSetterInstance() => this.CommonSetterInstance();
        [DebuggerStepThrough]
        object IPotionDataGetter.CommonSetterTranslationInstance() => this.CommonSetterTranslationInstance();

        #endregion

    }
}

#region Modules
#region Binary Translation
namespace Mutagen.Bethesda.Oblivion.Internals
{
    public partial class PotionDataBinaryWriteTranslation : IBinaryWriteTranslator
    {
        public readonly static PotionDataBinaryWriteTranslation Instance = new PotionDataBinaryWriteTranslation();

        public static void WriteEmbedded(
            IPotionDataGetter item,
            MutagenWriter writer)
        {
            writer.Write(item.Value);
            Mutagen.Bethesda.Binary.EnumBinaryTranslation<IngredientFlag>.Instance.Write(
                writer,
                item.Flags,
                length: 4);
        }

        public void Write(
            MutagenWriter writer,
            IPotionDataGetter item,
            RecordTypeConverter? recordTypeConverter = null)
        {
            using (HeaderExport.Header(
                writer: writer,
                record: recordTypeConverter.ConvertToCustom(RecordTypes.ENIT),
                type: Mutagen.Bethesda.Binary.ObjectType.Subrecord))
            {
                WriteEmbedded(
                    item: item,
                    writer: writer);
            }
        }

        public void Write(
            MutagenWriter writer,
            object item,
            RecordTypeConverter? recordTypeConverter = null)
        {
            Write(
                item: (IPotionDataGetter)item,
                writer: writer,
                recordTypeConverter: recordTypeConverter);
        }

    }

    public partial class PotionDataBinaryCreateTranslation
    {
        public readonly static PotionDataBinaryCreateTranslation Instance = new PotionDataBinaryCreateTranslation();

        public static void FillBinaryStructs(
            IPotionData item,
            MutagenFrame frame)
        {
            item.Value = frame.ReadUInt32();
            item.Flags = EnumBinaryTranslation<IngredientFlag>.Instance.Parse(frame: frame.SpawnWithLength(4));
        }

    }

}
namespace Mutagen.Bethesda.Oblivion
{
    #region Binary Write Mixins
    public static class PotionDataBinaryTranslationMixIn
    {
        public static void WriteToBinary(
            this IPotionDataGetter item,
            MutagenWriter writer,
            RecordTypeConverter? recordTypeConverter = null)
        {
            ((PotionDataBinaryWriteTranslation)item.BinaryWriteTranslator).Write(
                item: item,
                writer: writer,
                recordTypeConverter: recordTypeConverter);
        }

    }
    #endregion


}
namespace Mutagen.Bethesda.Oblivion.Internals
{
    public partial class PotionDataBinaryOverlay :
        BinaryOverlay,
        IPotionDataGetter
    {
        #region Common Routing
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILoquiRegistration ILoquiObject.Registration => PotionData_Registration.Instance;
        public static PotionData_Registration Registration => PotionData_Registration.Instance;
        [DebuggerStepThrough]
        protected object CommonInstance() => PotionDataCommon.Instance;
        [DebuggerStepThrough]
        protected object CommonSetterTranslationInstance() => PotionDataSetterTranslationCommon.Instance;
        [DebuggerStepThrough]
        object IPotionDataGetter.CommonInstance() => this.CommonInstance();
        [DebuggerStepThrough]
        object? IPotionDataGetter.CommonSetterInstance() => null;
        [DebuggerStepThrough]
        object IPotionDataGetter.CommonSetterTranslationInstance() => this.CommonSetterTranslationInstance();

        #endregion

        void IPrintable.ToString(FileGeneration fg, string? name) => this.ToString(fg, name);

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected object BinaryWriteTranslator => PotionDataBinaryWriteTranslation.Instance;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        object IBinaryItem.BinaryWriteTranslator => this.BinaryWriteTranslator;
        void IBinaryItem.WriteToBinary(
            MutagenWriter writer,
            RecordTypeConverter? recordTypeConverter = null)
        {
            ((PotionDataBinaryWriteTranslation)this.BinaryWriteTranslator).Write(
                item: this,
                writer: writer,
                recordTypeConverter: recordTypeConverter);
        }

        public UInt32 Value => BinaryPrimitives.ReadUInt32LittleEndian(_data.Slice(0x0, 0x4));
        public IngredientFlag Flags => (IngredientFlag)BinaryPrimitives.ReadInt32LittleEndian(_data.Span.Slice(0x4, 0x4));
        partial void CustomFactoryEnd(
            OverlayStream stream,
            int finalPos,
            int offset);

        partial void CustomCtor();
        protected PotionDataBinaryOverlay(
            ReadOnlyMemorySlice<byte> bytes,
            BinaryOverlayFactoryPackage package)
            : base(
                bytes: bytes,
                package: package)
        {
            this.CustomCtor();
        }

        public static PotionDataBinaryOverlay PotionDataFactory(
            OverlayStream stream,
            BinaryOverlayFactoryPackage package,
            RecordTypeConverter? recordTypeConverter = null)
        {
            var ret = new PotionDataBinaryOverlay(
                bytes: HeaderTranslation.ExtractSubrecordMemory(stream.RemainingMemory, package.MetaData.Constants),
                package: package);
            var finalPos = checked((int)(stream.Position + stream.GetSubrecord().TotalLength));
            int offset = stream.Position + package.MetaData.Constants.SubConstants.TypeAndLengthLength;
            stream.Position += 0x8 + package.MetaData.Constants.SubConstants.HeaderLength;
            ret.CustomFactoryEnd(
                stream: stream,
                finalPos: stream.Length,
                offset: offset);
            return ret;
        }

        public static PotionDataBinaryOverlay PotionDataFactory(
            ReadOnlyMemorySlice<byte> slice,
            BinaryOverlayFactoryPackage package,
            RecordTypeConverter? recordTypeConverter = null)
        {
            return PotionDataFactory(
                stream: new OverlayStream(slice, package),
                package: package,
                recordTypeConverter: recordTypeConverter);
        }

        #region To String

        public void ToString(
            FileGeneration fg,
            string? name = null)
        {
            PotionDataMixIn.ToString(
                item: this,
                name: name);
        }

        #endregion

        #region Equals and Hash
        public override bool Equals(object? obj)
        {
            if (!(obj is IPotionDataGetter rhs)) return false;
            return ((PotionDataCommon)((IPotionDataGetter)this).CommonInstance()!).Equals(this, rhs);
        }

        public bool Equals(IPotionDataGetter? obj)
        {
            return ((PotionDataCommon)((IPotionDataGetter)this).CommonInstance()!).Equals(this, obj);
        }

        public override int GetHashCode() => ((PotionDataCommon)((IPotionDataGetter)this).CommonInstance()!).GetHashCode(this);

        #endregion

    }

}
#endregion

#endregion

