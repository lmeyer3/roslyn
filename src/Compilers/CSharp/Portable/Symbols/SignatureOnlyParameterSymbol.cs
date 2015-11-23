﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
    /// <summary>
    /// Intended to be used to create ParameterSymbols for a SignatureOnlyMethodSymbol.
    /// </summary>
    internal sealed class SignatureOnlyParameterSymbol : ParameterSymbol
    {
        private readonly TypeSymbolWithAnnotations _type;
        private readonly bool _isParams;
        private readonly RefKind _refKind;

        public SignatureOnlyParameterSymbol(
            TypeSymbolWithAnnotations type,
            bool isParams,
            RefKind refKind)
        {
            Debug.Assert((object)type != null);

            _type = type;
            _isParams = isParams;
            _refKind = refKind;
        }

        public override TypeSymbolWithAnnotations Type { get { return _type; } }

        public override bool IsParams { get { return _isParams; } }

        public override RefKind RefKind { get { return _refKind; } }

        public override string Name { get { return ""; } }

        public override bool IsImplicitlyDeclared
        {
            get { return true; }
        }

        #region Not used by MethodSignatureComparer

        internal override bool IsMetadataIn { get { throw ExceptionUtilities.Unreachable; } }

        internal override bool IsMetadataOut { get { throw ExceptionUtilities.Unreachable; } }

        internal override MarshalPseudoCustomAttributeData MarshallingInformation { get { throw ExceptionUtilities.Unreachable; } }

        public override int Ordinal { get { throw ExceptionUtilities.Unreachable; } }

        internal override bool IsMetadataOptional { get { throw ExceptionUtilities.Unreachable; } }

        internal override ConstantValue ExplicitDefaultConstantValue { get { throw ExceptionUtilities.Unreachable; } }

        internal override bool IsIDispatchConstant { get { throw ExceptionUtilities.Unreachable; } }

        internal override bool IsIUnknownConstant { get { throw ExceptionUtilities.Unreachable; } }

        internal override bool IsCallerFilePath { get { throw ExceptionUtilities.Unreachable; } }

        internal override bool IsCallerLineNumber { get { throw ExceptionUtilities.Unreachable; } }

        internal override bool IsCallerMemberName { get { throw ExceptionUtilities.Unreachable; } }

        internal sealed override ushort CountOfCustomModifiersPrecedingByRef { get { return 0; } }

        public override Symbol ContainingSymbol { get { throw ExceptionUtilities.Unreachable; } }

        public override ImmutableArray<Location> Locations { get { throw ExceptionUtilities.Unreachable; } }

        public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences { get { throw ExceptionUtilities.Unreachable; } }

        public override AssemblySymbol ContainingAssembly { get { throw ExceptionUtilities.Unreachable; } }

        internal override ModuleSymbol ContainingModule { get { throw ExceptionUtilities.Unreachable; } }

        #endregion Not used by MethodSignatureComparer

        public override bool Equals(object obj)
        {
            if ((object)this == obj)
            {
                return true;
            }

            var other = obj as SignatureOnlyParameterSymbol;
            return (object)other != null &&
                _type == other._type &&
                _customModifiers.Equals(other._customModifiers) &&
                _isParams == other._isParams &&
                _refKind == other._refKind;
        }

        public override int GetHashCode()
        {
            return Hash.Combine(
                _type.GetHashCode(),
                Hash.Combine(
                    Hash.CombineValues(_customModifiers),
                    Hash.Combine(
                        _isParams.GetHashCode(),
                        _refKind.GetHashCode())));
        }
    }
}
