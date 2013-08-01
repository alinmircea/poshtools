﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace PowerShellTools
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("PowerShell")]
    [TagType(typeof(PowerShellTokenTag))]
    internal sealed class PowerShellTokenTagProvider : ITaggerProvider
    {

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return new PowerShellTokenTagger(buffer) as ITagger<T>;
        }
    }

    public class PowerShellTokenTag : ITag
    {
        public PSTokenType type { get; private set; }

        public PowerShellTokenTag(PSTokenType type)
        {
            this.type = type;
        }
    }

    internal sealed class PowerShellTokenTagger : ITagger<PowerShellTokenTag>
    {

        ITextBuffer _buffer;

        internal PowerShellTokenTagger(ITextBuffer buffer)
        {
            _buffer = buffer;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<PowerShellTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (SnapshotSpan curSpan in spans)
            {
                ITextSnapshotLine containingLine = curSpan.Start.GetContainingLine();
                int curLoc = containingLine.Start.Position;

                Collection<PSParseError> errors = new Collection<PSParseError>();
                var tokens = PSParser.Tokenize(containingLine.GetText(), out errors);

                foreach (var token in tokens)
                {
                        var tokenSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curLoc + token.Start, token.Length));
                        if (tokenSpan.IntersectsWith(curSpan))
                            yield return new TagSpan<PowerShellTokenTag>(tokenSpan,
                                                                  new PowerShellTokenTag(token.Type));
                }
            }

        }
    }
}
