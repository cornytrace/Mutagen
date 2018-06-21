/*
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Autogenerated by Loqui.  Do not manually change.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loqui;
using Noggog;
using Noggog.Notifying;
using Mutagen.Bethesda.Oblivion.Internals;
using Mutagen.Bethesda.Oblivion;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Internals;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Noggog.Xml;
using Loqui.Xml;
using Loqui.Internal;
using System.Diagnostics;
using Mutagen.Bethesda.Binary;

namespace Mutagen.Bethesda.Oblivion
{
    #region Class
    public abstract partial class NPCAbstract : NPCSpawn, INPCAbstract, ILoquiObject<NPCAbstract>, ILoquiObjectSetter, IEquatable<NPCAbstract>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ILoquiRegistration ILoquiObject.Registration => NPCAbstract_Registration.Instance;
        public new static NPCAbstract_Registration Registration => NPCAbstract_Registration.Instance;

        #region Ctor
        public NPCAbstract()
        {
            CustomCtor();
        }
        partial void CustomCtor();
        #endregion


        #region Loqui Getter Interface

        protected override object GetNthObject(ushort index) => NPCAbstractCommon.GetNthObject(index, this);

        protected override bool GetNthObjectHasBeenSet(ushort index) => NPCAbstractCommon.GetNthObjectHasBeenSet(index, this);

        protected override void UnsetNthObject(ushort index, NotifyingUnsetParameters cmds) => NPCAbstractCommon.UnsetNthObject(index, this, cmds);

        #endregion

        #region Loqui Interface
        protected override void SetNthObjectHasBeenSet(ushort index, bool on)
        {
            NPCAbstractCommon.SetNthObjectHasBeenSet(index, on, this);
        }

        #endregion

        IMask<bool> IEqualsMask<NPCAbstract>.GetEqualsMask(NPCAbstract rhs) => NPCAbstractCommon.GetEqualsMask(this, rhs);
        IMask<bool> IEqualsMask<INPCAbstractGetter>.GetEqualsMask(INPCAbstractGetter rhs) => NPCAbstractCommon.GetEqualsMask(this, rhs);
        #region To String
        public override string ToString()
        {
            return NPCAbstractCommon.ToString(this, printMask: null);
        }

        public string ToString(
            string name = null,
            NPCAbstract_Mask<bool> printMask = null)
        {
            return NPCAbstractCommon.ToString(this, name: name, printMask: printMask);
        }

        public override void ToString(
            FileGeneration fg,
            string name = null)
        {
            NPCAbstractCommon.ToString(this, fg, name: name, printMask: null);
        }

        #endregion

        IMask<bool> ILoquiObjectGetter.GetHasBeenSetMask() => this.GetHasBeenSetMask();
        public new NPCAbstract_Mask<bool> GetHasBeenSetMask()
        {
            return NPCAbstractCommon.GetHasBeenSetMask(this);
        }
        #region Equals and Hash
        public override bool Equals(object obj)
        {
            if (!(obj is NPCAbstract rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(NPCAbstract rhs)
        {
            if (rhs == null) return false;
            if (!base.Equals(rhs)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int ret = 0;
            ret = ret.CombineHashCode(base.GetHashCode());
            return ret;
        }

        #endregion


        #region XML Translation
        #region XML Copy In
        public override void CopyIn_XML(
            XElement root,
            NotifyingFireParameters cmds = null)
        {
            LoquiXmlTranslation<NPCAbstract>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                errorMask: null,
                cmds: cmds);
        }

        public virtual void CopyIn_XML(
            XElement root,
            out NPCAbstract_ErrorMask errorMask,
            NotifyingFireParameters cmds = null)
        {
            ErrorMaskBuilder errorMaskBuilder = new ErrorMaskBuilder();
            LoquiXmlTranslation<NPCAbstract>.Instance.CopyIn(
                root: root,
                item: this,
                skipProtected: true,
                errorMask: errorMaskBuilder,
                cmds: cmds);
            errorMask = NPCAbstract_ErrorMask.Factory(errorMaskBuilder);
        }

        public void CopyIn_XML(
            string path,
            NotifyingFireParameters cmds = null)
        {
            var root = XDocument.Load(path).Root;
            this.CopyIn_XML(
                root: root,
                cmds: cmds);
        }

        public void CopyIn_XML(
            string path,
            out NPCAbstract_ErrorMask errorMask,
            NotifyingFireParameters cmds = null)
        {
            var root = XDocument.Load(path).Root;
            this.CopyIn_XML(
                root: root,
                errorMask: out errorMask,
                cmds: cmds);
        }

        public void CopyIn_XML(
            Stream stream,
            NotifyingFireParameters cmds = null)
        {
            var root = XDocument.Load(stream).Root;
            this.CopyIn_XML(
                root: root,
                cmds: cmds);
        }

        public void CopyIn_XML(
            Stream stream,
            out NPCAbstract_ErrorMask errorMask,
            NotifyingFireParameters cmds = null)
        {
            var root = XDocument.Load(stream).Root;
            this.CopyIn_XML(
                root: root,
                errorMask: out errorMask,
                cmds: cmds);
        }

        public override void CopyIn_XML(
            XElement root,
            out NPCSpawn_ErrorMask errorMask,
            NotifyingFireParameters cmds = null)
        {
            this.CopyIn_XML(
                root: root,
                errorMask: out NPCAbstract_ErrorMask errMask,
                cmds: cmds);
            errorMask = errMask;
        }

        public override void CopyIn_XML(
            XElement root,
            out MajorRecord_ErrorMask errorMask,
            NotifyingFireParameters cmds = null)
        {
            this.CopyIn_XML(
                root: root,
                errorMask: out NPCAbstract_ErrorMask errMask,
                cmds: cmds);
            errorMask = errMask;
        }

        #endregion

        #region XML Write
        public virtual void Write_XML(
            XElement node,
            out NPCAbstract_ErrorMask errorMask,
            bool doMasks = true,
            string name = null)
        {
            ErrorMaskBuilder errorMaskBuilder = doMasks ? new ErrorMaskBuilder() : null;
            this.Write_XML_Internal(
                node: node,
                name: name,
                errorMask: errorMaskBuilder);
            errorMask = NPCAbstract_ErrorMask.Factory(errorMaskBuilder);
        }

        public virtual void Write_XML(
            string path,
            out NPCAbstract_ErrorMask errorMask,
            bool doMasks = true,
            string name = null)
        {
            XElement topNode = new XElement("topnode");
            Write_XML(
                node: topNode,
                name: name,
                errorMask: out errorMask,
                doMasks: doMasks);
            topNode.Elements().First().Save(path);
        }

        public virtual void Write_XML(
            Stream stream,
            out NPCAbstract_ErrorMask errorMask,
            bool doMasks = true,
            string name = null)
        {
            XElement topNode = new XElement("topnode");
            Write_XML(
                node: topNode,
                name: name,
                errorMask: out errorMask,
                doMasks: doMasks);
            topNode.Elements().First().Save(stream);
        }

        protected override void Write_XML_Internal(
            XElement node,
            ErrorMaskBuilder errorMask,
            string name = null)
        {
            NPCAbstractCommon.Write_XML(
                item: this,
                node: node,
                name: name,
                errorMask: errorMask);
        }
        #endregion

        protected static void Fill_XML_Internal(
            NPCAbstract item,
            XElement root,
            string name,
            ErrorMaskBuilder errorMask)
        {
            switch (name)
            {
                default:
                    NPCSpawn.Fill_XML_Internal(
                        item: item,
                        root: root,
                        name: name,
                        errorMask: errorMask);
                    break;
            }
        }

        #endregion

        #region Binary Translation
        #region Binary Write
        public virtual void Write_Binary(
            MutagenWriter writer,
            out NPCAbstract_ErrorMask errorMask,
            bool doMasks = true)
        {
            ErrorMaskBuilder errorMaskBuilder = doMasks ? new ErrorMaskBuilder() : null;
            this.Write_Binary_Internal(
                writer: writer,
                recordTypeConverter: null,
                errorMask: errorMaskBuilder);
            errorMask = NPCAbstract_ErrorMask.Factory(errorMaskBuilder);
        }

        public virtual void Write_Binary(
            string path,
            out NPCAbstract_ErrorMask errorMask,
            bool doMasks = true)
        {
            using (var writer = new MutagenWriter(path))
            {
                Write_Binary(
                    writer: writer,
                    errorMask: out errorMask,
                    doMasks: doMasks);
            }
        }

        public virtual void Write_Binary(
            Stream stream,
            out NPCAbstract_ErrorMask errorMask,
            bool doMasks = true)
        {
            using (var writer = new MutagenWriter(stream))
            {
                Write_Binary(
                    writer: writer,
                    errorMask: out errorMask,
                    doMasks: doMasks);
            }
        }

        protected override void Write_Binary_Internal(
            MutagenWriter writer,
            RecordTypeConverter recordTypeConverter,
            ErrorMaskBuilder errorMask)
        {
            NPCAbstractCommon.Write_Binary(
                item: this,
                writer: writer,
                recordTypeConverter: recordTypeConverter,
                errorMask: errorMask);
        }
        #endregion

        #endregion

        public NPCAbstract Copy(
            NPCAbstract_CopyMask copyMask = null,
            INPCAbstractGetter def = null)
        {
            return NPCAbstract.Copy(
                this,
                copyMask: copyMask,
                def: def);
        }

        public static NPCAbstract Copy(
            INPCAbstract item,
            NPCAbstract_CopyMask copyMask = null,
            INPCAbstractGetter def = null)
        {
            NPCAbstract ret = (NPCAbstract)System.Activator.CreateInstance(item.GetType());
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        public static NPCAbstract Copy_ToLoqui(
            INPCAbstractGetter item,
            NPCAbstract_CopyMask copyMask = null,
            INPCAbstractGetter def = null)
        {
            NPCAbstract ret = (NPCAbstract)System.Activator.CreateInstance(item.GetType());
            ret.CopyFieldsFrom(
                item,
                copyMask: copyMask,
                def: def);
            return ret;
        }

        public void CopyFieldsFrom(
            INPCAbstractGetter rhs,
            NPCAbstract_CopyMask copyMask,
            INPCAbstractGetter def = null,
            NotifyingFireParameters cmds = null)
        {
            this.CopyFieldsFrom(
                rhs: rhs,
                def: def,
                doMasks: false,
                errorMask: out var errMask,
                copyMask: copyMask,
                cmds: cmds);
        }

        public void CopyFieldsFrom(
            INPCAbstractGetter rhs,
            out NPCAbstract_ErrorMask errorMask,
            NPCAbstract_CopyMask copyMask = null,
            INPCAbstractGetter def = null,
            NotifyingFireParameters cmds = null,
            bool doMasks = true)
        {
            var errorMaskBuilder = new ErrorMaskBuilder();
            NPCAbstractCommon.CopyFieldsFrom(
                item: this,
                rhs: rhs,
                def: def,
                errorMask: errorMaskBuilder,
                copyMask: copyMask,
                cmds: cmds);
            errorMask = NPCAbstract_ErrorMask.Factory(errorMaskBuilder);
        }

        public void CopyFieldsFrom(
            INPCAbstractGetter rhs,
            ErrorMaskBuilder errorMask,
            NPCAbstract_CopyMask copyMask = null,
            INPCAbstractGetter def = null,
            NotifyingFireParameters cmds = null,
            bool doMasks = true)
        {
            NPCAbstractCommon.CopyFieldsFrom(
                item: this,
                rhs: rhs,
                def: def,
                errorMask: errorMask,
                copyMask: copyMask,
                cmds: cmds);
        }

        protected override void SetNthObject(ushort index, object obj, NotifyingFireParameters cmds = null)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    base.SetNthObject(index, obj, cmds);
                    break;
            }
        }

        public override void Clear(NotifyingUnsetParameters cmds = null)
        {
            CallClearPartial_Internal(cmds);
            NPCAbstractCommon.Clear(this, cmds);
        }


        protected new static void CopyInInternal_NPCAbstract(NPCAbstract obj, KeyValuePair<ushort, object> pair)
        {
            if (!EnumExt.TryParse(pair.Key, out NPCAbstract_FieldIndex enu))
            {
                CopyInInternal_NPCSpawn(obj, pair);
            }
            switch (enu)
            {
                default:
                    throw new ArgumentException($"Unknown enum type: {enu}");
            }
        }
        public static void CopyIn(IEnumerable<KeyValuePair<ushort, object>> fields, NPCAbstract obj)
        {
            ILoquiObjectExt.CopyFieldsIn(obj, fields, def: null, skipProtected: false, cmds: null);
        }

    }
    #endregion

    #region Interface
    public partial interface INPCAbstract : INPCAbstractGetter, INPCSpawn, ILoquiClass<INPCAbstract, INPCAbstractGetter>, ILoquiClass<NPCAbstract, INPCAbstractGetter>
    {
    }

    public partial interface INPCAbstractGetter : INPCSpawnGetter
    {

    }

    #endregion

}

namespace Mutagen.Bethesda.Oblivion.Internals
{
    #region Field Index
    public enum NPCAbstract_FieldIndex
    {
        MajorRecordFlags = 0,
        FormID = 1,
        Version = 2,
        EditorID = 3,
        RecordType = 4,
    }
    #endregion

    #region Registration
    public class NPCAbstract_Registration : ILoquiRegistration
    {
        public static readonly NPCAbstract_Registration Instance = new NPCAbstract_Registration();

        public static ProtocolKey ProtocolKey => ProtocolDefinition_Oblivion.ProtocolKey;

        public static readonly ObjectKey ObjectKey = new ObjectKey(
            protocolKey: ProtocolDefinition_Oblivion.ProtocolKey,
            msgID: 94,
            version: 0);

        public const string GUID = "64c259ed-5c42-4608-8c24-a41b06215b07";

        public const ushort FieldCount = 0;

        public static readonly Type MaskType = typeof(NPCAbstract_Mask<>);

        public static readonly Type ErrorMaskType = typeof(NPCAbstract_ErrorMask);

        public static readonly Type ClassType = typeof(NPCAbstract);

        public static readonly Type GetterType = typeof(INPCAbstractGetter);

        public static readonly Type SetterType = typeof(INPCAbstract);

        public static readonly Type CommonType = typeof(NPCAbstractCommon);

        public const string FullName = "Mutagen.Bethesda.Oblivion.NPCAbstract";

        public const string Name = "NPCAbstract";

        public const string Namespace = "Mutagen.Bethesda.Oblivion";

        public const byte GenericCount = 0;

        public static readonly Type GenericRegistrationType = null;

        public static ushort? GetNameIndex(StringCaseAgnostic str)
        {
            switch (str.Upper)
            {
                default:
                    return null;
            }
        }

        public static bool GetNthIsEnumerable(ushort index)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return NPCSpawn_Registration.GetNthIsEnumerable(index);
            }
        }

        public static bool GetNthIsLoqui(ushort index)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return NPCSpawn_Registration.GetNthIsLoqui(index);
            }
        }

        public static bool GetNthIsSingleton(ushort index)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return NPCSpawn_Registration.GetNthIsSingleton(index);
            }
        }

        public static string GetNthName(ushort index)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return NPCSpawn_Registration.GetNthName(index);
            }
        }

        public static bool IsNthDerivative(ushort index)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return NPCSpawn_Registration.IsNthDerivative(index);
            }
        }

        public static bool IsProtected(ushort index)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return NPCSpawn_Registration.IsProtected(index);
            }
        }

        public static Type GetNthType(ushort index)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return NPCSpawn_Registration.GetNthType(index);
            }
        }

        public const int NumStructFields = 0;
        public const int NumTypedFields = 0;
        #region Interface
        ProtocolKey ILoquiRegistration.ProtocolKey => ProtocolKey;
        ObjectKey ILoquiRegistration.ObjectKey => ObjectKey;
        string ILoquiRegistration.GUID => GUID;
        int ILoquiRegistration.FieldCount => FieldCount;
        Type ILoquiRegistration.MaskType => MaskType;
        Type ILoquiRegistration.ErrorMaskType => ErrorMaskType;
        Type ILoquiRegistration.ClassType => ClassType;
        Type ILoquiRegistration.SetterType => SetterType;
        Type ILoquiRegistration.GetterType => GetterType;
        Type ILoquiRegistration.CommonType => CommonType;
        string ILoquiRegistration.FullName => FullName;
        string ILoquiRegistration.Name => Name;
        string ILoquiRegistration.Namespace => Namespace;
        byte ILoquiRegistration.GenericCount => GenericCount;
        Type ILoquiRegistration.GenericRegistrationType => GenericRegistrationType;
        ushort? ILoquiRegistration.GetNameIndex(StringCaseAgnostic name) => GetNameIndex(name);
        bool ILoquiRegistration.GetNthIsEnumerable(ushort index) => GetNthIsEnumerable(index);
        bool ILoquiRegistration.GetNthIsLoqui(ushort index) => GetNthIsLoqui(index);
        bool ILoquiRegistration.GetNthIsSingleton(ushort index) => GetNthIsSingleton(index);
        string ILoquiRegistration.GetNthName(ushort index) => GetNthName(index);
        bool ILoquiRegistration.IsNthDerivative(ushort index) => IsNthDerivative(index);
        bool ILoquiRegistration.IsProtected(ushort index) => IsProtected(index);
        Type ILoquiRegistration.GetNthType(ushort index) => GetNthType(index);
        #endregion

    }
    #endregion

    #region Extensions
    public static partial class NPCAbstractCommon
    {
        #region Copy Fields From
        public static void CopyFieldsFrom(
            INPCAbstract item,
            INPCAbstractGetter rhs,
            INPCAbstractGetter def,
            ErrorMaskBuilder errorMask,
            NPCAbstract_CopyMask copyMask,
            NotifyingFireParameters cmds = null)
        {
            NPCSpawnCommon.CopyFieldsFrom(
                item,
                rhs,
                def,
                errorMask,
                copyMask,
                cmds);
        }

        #endregion

        public static void SetNthObjectHasBeenSet(
            ushort index,
            bool on,
            INPCAbstract obj,
            NotifyingFireParameters cmds = null)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    NPCSpawnCommon.SetNthObjectHasBeenSet(index, on, obj);
                    break;
            }
        }

        public static void UnsetNthObject(
            ushort index,
            INPCAbstract obj,
            NotifyingUnsetParameters cmds = null)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    NPCSpawnCommon.UnsetNthObject(index, obj);
                    break;
            }
        }

        public static bool GetNthObjectHasBeenSet(
            ushort index,
            INPCAbstract obj)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return NPCSpawnCommon.GetNthObjectHasBeenSet(index, obj);
            }
        }

        public static object GetNthObject(
            ushort index,
            INPCAbstractGetter obj)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return NPCSpawnCommon.GetNthObject(index, obj);
            }
        }

        public static void Clear(
            INPCAbstract item,
            NotifyingUnsetParameters cmds = null)
        {
        }

        public static NPCAbstract_Mask<bool> GetEqualsMask(
            this INPCAbstractGetter item,
            INPCAbstractGetter rhs)
        {
            var ret = new NPCAbstract_Mask<bool>();
            FillEqualsMask(item, rhs, ret);
            return ret;
        }

        public static void FillEqualsMask(
            INPCAbstractGetter item,
            INPCAbstractGetter rhs,
            NPCAbstract_Mask<bool> ret)
        {
            if (rhs == null) return;
            NPCSpawnCommon.FillEqualsMask(item, rhs, ret);
        }

        public static string ToString(
            this INPCAbstractGetter item,
            string name = null,
            NPCAbstract_Mask<bool> printMask = null)
        {
            var fg = new FileGeneration();
            item.ToString(fg, name, printMask);
            return fg.ToString();
        }

        public static void ToString(
            this INPCAbstractGetter item,
            FileGeneration fg,
            string name = null,
            NPCAbstract_Mask<bool> printMask = null)
        {
            if (name == null)
            {
                fg.AppendLine($"{nameof(NPCAbstract)} =>");
            }
            else
            {
                fg.AppendLine($"{name} ({nameof(NPCAbstract)}) =>");
            }
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
            }
            fg.AppendLine("]");
        }

        public static bool HasBeenSet(
            this INPCAbstractGetter item,
            NPCAbstract_Mask<bool?> checkMask)
        {
            return true;
        }

        public static NPCAbstract_Mask<bool> GetHasBeenSetMask(INPCAbstractGetter item)
        {
            var ret = new NPCAbstract_Mask<bool>();
            return ret;
        }

        public static NPCAbstract_FieldIndex? ConvertFieldIndex(NPCSpawn_FieldIndex? index)
        {
            if (!index.HasValue) return null;
            return ConvertFieldIndex(index: index.Value);
        }

        public static NPCAbstract_FieldIndex ConvertFieldIndex(NPCSpawn_FieldIndex index)
        {
            switch (index)
            {
                case NPCSpawn_FieldIndex.MajorRecordFlags:
                    return (NPCAbstract_FieldIndex)((int)index);
                case NPCSpawn_FieldIndex.FormID:
                    return (NPCAbstract_FieldIndex)((int)index);
                case NPCSpawn_FieldIndex.Version:
                    return (NPCAbstract_FieldIndex)((int)index);
                case NPCSpawn_FieldIndex.EditorID:
                    return (NPCAbstract_FieldIndex)((int)index);
                case NPCSpawn_FieldIndex.RecordType:
                    return (NPCAbstract_FieldIndex)((int)index);
                default:
                    throw new ArgumentException($"Index is out of range: {index.ToStringFast_Enum_Only()}");
            }
        }

        public static NPCAbstract_FieldIndex? ConvertFieldIndex(MajorRecord_FieldIndex? index)
        {
            if (!index.HasValue) return null;
            return ConvertFieldIndex(index: index.Value);
        }

        public static NPCAbstract_FieldIndex ConvertFieldIndex(MajorRecord_FieldIndex index)
        {
            switch (index)
            {
                case MajorRecord_FieldIndex.MajorRecordFlags:
                    return (NPCAbstract_FieldIndex)((int)index);
                case MajorRecord_FieldIndex.FormID:
                    return (NPCAbstract_FieldIndex)((int)index);
                case MajorRecord_FieldIndex.Version:
                    return (NPCAbstract_FieldIndex)((int)index);
                case MajorRecord_FieldIndex.EditorID:
                    return (NPCAbstract_FieldIndex)((int)index);
                case MajorRecord_FieldIndex.RecordType:
                    return (NPCAbstract_FieldIndex)((int)index);
                default:
                    throw new ArgumentException($"Index is out of range: {index.ToStringFast_Enum_Only()}");
            }
        }

        #region XML Translation
        #region XML Write
        public static void Write_XML(
            XElement node,
            INPCAbstractGetter item,
            bool doMasks,
            out NPCAbstract_ErrorMask errorMask,
            string name = null)
        {
            ErrorMaskBuilder errorMaskBuilder = doMasks ? new ErrorMaskBuilder() : null;
            Write_XML(
                node: node,
                name: name,
                item: item,
                errorMask: errorMaskBuilder);
            errorMask = NPCAbstract_ErrorMask.Factory(errorMaskBuilder);
        }

        public static void Write_XML(
            XElement node,
            INPCAbstractGetter item,
            ErrorMaskBuilder errorMask,
            string name = null)
        {
            var elem = new XElement(name ?? "Mutagen.Bethesda.Oblivion.NPCAbstract");
            node.Add(elem);
            if (name != null)
            {
                elem.SetAttributeValue("type", "Mutagen.Bethesda.Oblivion.NPCAbstract");
            }
        }
        #endregion

        #endregion

        #region Binary Translation
        #region Binary Write
        public static void Write_Binary(
            MutagenWriter writer,
            NPCAbstract item,
            RecordTypeConverter recordTypeConverter,
            bool doMasks,
            out NPCAbstract_ErrorMask errorMask)
        {
            ErrorMaskBuilder errorMaskBuilder = doMasks ? new ErrorMaskBuilder() : null;
            Write_Binary(
                writer: writer,
                item: item,
                recordTypeConverter: recordTypeConverter,
                errorMask: errorMaskBuilder);
            errorMask = NPCAbstract_ErrorMask.Factory(errorMaskBuilder);
        }

        public static void Write_Binary(
            MutagenWriter writer,
            NPCAbstract item,
            RecordTypeConverter recordTypeConverter,
            ErrorMaskBuilder errorMask)
        {
            MajorRecordCommon.Write_Binary_Embedded(
                item: item,
                writer: writer,
                errorMask: errorMask);
            MajorRecordCommon.Write_Binary_RecordTypes(
                item: item,
                writer: writer,
                recordTypeConverter: recordTypeConverter,
                errorMask: errorMask);
        }
        #endregion

        #endregion

    }
    #endregion

    #region Modules

    #region Mask
    public class NPCAbstract_Mask<T> : NPCSpawn_Mask<T>, IMask<T>, IEquatable<NPCAbstract_Mask<T>>
    {
        #region Ctors
        public NPCAbstract_Mask()
        {
        }

        public NPCAbstract_Mask(T initialValue)
        {
        }
        #endregion

        #region Equals
        public override bool Equals(object obj)
        {
            if (!(obj is NPCAbstract_Mask<T> rhs)) return false;
            return Equals(rhs);
        }

        public bool Equals(NPCAbstract_Mask<T> rhs)
        {
            if (rhs == null) return false;
            if (!base.Equals(rhs)) return false;
            return true;
        }
        public override int GetHashCode()
        {
            int ret = 0;
            ret = ret.CombineHashCode(base.GetHashCode());
            return ret;
        }

        #endregion

        #region All Equal
        public override bool AllEqual(Func<T, bool> eval)
        {
            if (!base.AllEqual(eval)) return false;
            return true;
        }
        #endregion

        #region Translate
        public new NPCAbstract_Mask<R> Translate<R>(Func<T, R> eval)
        {
            var ret = new NPCAbstract_Mask<R>();
            this.Translate_InternalFill(ret, eval);
            return ret;
        }

        protected void Translate_InternalFill<R>(NPCAbstract_Mask<R> obj, Func<T, R> eval)
        {
            base.Translate_InternalFill(obj, eval);
        }
        #endregion

        #region Clear Enumerables
        public override void ClearEnumerables()
        {
            base.ClearEnumerables();
        }
        #endregion

        #region To String
        public override string ToString()
        {
            return ToString(printMask: null);
        }

        public string ToString(NPCAbstract_Mask<bool> printMask = null)
        {
            var fg = new FileGeneration();
            ToString(fg, printMask);
            return fg.ToString();
        }

        public void ToString(FileGeneration fg, NPCAbstract_Mask<bool> printMask = null)
        {
            fg.AppendLine($"{nameof(NPCAbstract_Mask<T>)} =>");
            fg.AppendLine("[");
            using (new DepthWrapper(fg))
            {
            }
            fg.AppendLine("]");
        }
        #endregion

    }

    public class NPCAbstract_ErrorMask : NPCSpawn_ErrorMask, IErrorMask<NPCAbstract_ErrorMask>
    {
        #region IErrorMask
        public override object GetNthMask(int index)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    return base.GetNthMask(index);
            }
        }

        public override void SetNthException(int index, Exception ex)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    base.SetNthException(index, ex);
                    break;
            }
        }

        public override void SetNthMask(int index, object obj)
        {
            NPCAbstract_FieldIndex enu = (NPCAbstract_FieldIndex)index;
            switch (enu)
            {
                default:
                    base.SetNthMask(index, obj);
                    break;
            }
        }

        public override bool IsInError()
        {
            if (Overall != null) return true;
            return false;
        }
        #endregion

        #region To String
        public override string ToString()
        {
            var fg = new FileGeneration();
            ToString(fg);
            return fg.ToString();
        }

        public override void ToString(FileGeneration fg)
        {
            fg.AppendLine("NPCAbstract_ErrorMask =>");
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
        protected override void ToString_FillInternal(FileGeneration fg)
        {
            base.ToString_FillInternal(fg);
        }
        #endregion

        #region Combine
        public NPCAbstract_ErrorMask Combine(NPCAbstract_ErrorMask rhs)
        {
            var ret = new NPCAbstract_ErrorMask();
            return ret;
        }
        public static NPCAbstract_ErrorMask Combine(NPCAbstract_ErrorMask lhs, NPCAbstract_ErrorMask rhs)
        {
            if (lhs != null && rhs != null) return lhs.Combine(rhs);
            return lhs ?? rhs;
        }
        #endregion

        #region Factory
        public static NPCAbstract_ErrorMask Factory(ErrorMaskBuilder errorMask)
        {
            if (errorMask?.Empty ?? true) return null;
            return new NPCAbstract_ErrorMask();
        }
        #endregion

    }
    public class NPCAbstract_CopyMask : NPCSpawn_CopyMask
    {
    }
    #endregion




    #endregion

}
