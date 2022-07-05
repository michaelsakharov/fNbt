using System;
using System.Text;
using JetBrains.Annotations;

namespace fNbt {
    /// <summary> A tag containing a 128 bit floating point number. </summary>
    public sealed class NbtDecimal : NbtTag {
        /// <summary> Type of this tag (Decimal). </summary>
        public override NbtTagType TagType {
            get {
                return NbtTagType.Decimal;
            }
        }

        /// <summary> Value/payload of this tag (a 128 bit floating point number). </summary>
        public decimal Value { get; set; }


        /// <summary> Creates an unnamed NbtDecimal tag with the default value of 0. </summary>
        public NbtDecimal() {}


        /// <summary> Creates an unnamed NbtDecimal tag with the given value. </summary>
        /// <param name="value"> Value to assign to this tag. </param>
        public NbtDecimal(decimal value )
            : this( null, value ) {}


        /// <summary> Creates an NbtDecimal tag with the given name and the default value of 0. </summary>
        /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
        public NbtDecimal( [CanBeNull] string tagName )
            : this( tagName, 0 ) {}


        /// <summary> Creates an NbtDecimal tag with the given name and value. </summary>
        /// <param name="tagName"> Name to assign to this tag. May be <c>null</c>. </param>
        /// <param name="value"> Value to assign to this tag. </param>
        public NbtDecimal( [CanBeNull] string tagName, decimal value ) {
            Name = tagName;
            Value = value;
        }


        internal override bool ReadTag( NbtBinaryReader readStream ) {
            if( readStream.Selector != null && !readStream.Selector( this ) ) {
                readStream.ReadDecimal();
                return false;
            }
            Value = readStream.ReadDecimal();
            return true;
        }


        internal override void SkipTag( NbtBinaryReader readStream ) {
            readStream.ReadDecimal();
        }


        internal override void WriteTag( NbtBinaryWriter writeStream, bool writeName ) {
            writeStream.Write( NbtTagType.Decimal);
            if( writeName ) {
                if( Name == null )
                    throw new NbtFormatException( "Name is null" );
                writeStream.Write( Name );
            }
            writeStream.Write( Value );
        }


        internal override void WriteData( NbtBinaryWriter writeStream ) {
            writeStream.Write( Value );
        }


        /// <summary> Returns a String that represents the current NbtDecimal object.
        /// Format: TAG_Decimal("Name"): Value </summary>
        /// <returns> A String that represents the current NbtDecimal object. </returns>
        public override string ToString() {
            var sb = new StringBuilder();
            PrettyPrint( sb, null, 0 );
            return sb.ToString();
        }


        internal override void PrettyPrint( StringBuilder sb, string indentString, int indentLevel ) {
            for( int i = 0; i < indentLevel; i++ ) {
                sb.Append( indentString );
            }
            sb.Append("TAG_Decimal");
            if( !String.IsNullOrEmpty( Name ) ) {
                sb.AppendFormat( "(\"{0}\")", Name );
            }
            sb.Append( ": " );
            sb.Append( Value );
        }
    }
}