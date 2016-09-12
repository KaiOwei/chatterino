﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatterino.Common;

namespace Chatterino.Controls
{
    public class EmoteList : MessageContainerControl
    {
        private Message[] _messages = new Message[0];

        protected override Message[] Messages
        {
            get
            {
                return _messages;
            }
        }

        public EmoteList()
        {
            mouseScrollMultiplyer = 0.2;
        }

        public void LoadChannel(TwitchChannel channel)
        {
            lock (MessageLock)
            {
                List<Message> messages = new List<Message>();

                // twitch emotes
                {
                    List<Word> words = new List<Word>();

                    foreach (var emote in Emotes.TwitchEmotes.OrderBy(x => x.Key))
                    {
                        words.Add(new Word { Type = SpanType.Emote, Value = Emotes.GetTwitchEmoteById(emote.Value, emote.Key), Tooltip = emote.Key + "\nTwitch Emote", CopyText = emote.Key, Link = "@insertText|" + emote.Key + " " });
                    }

                    if (words.Count != 0)
                    {
                        messages.Add(new Message("Twitch Emotes"));
                        messages.Add(new Message(words));
                    }
                }

                // bttv channel emotes
                if (channel != null)
                {
                    List<Word> words = new List<Word>();

                    foreach (var emote in channel.BttvChannelEmotes.Values)
                    {
                        words.Add(new Word { Type = SpanType.Emote, Value = emote, Tooltip = emote.Tooltip, CopyText = emote.Name, Link = "@insertText|" + emote.Name + " " });
                    }

                    if (words.Count != 0)
                    {
                        messages.Add(new Message("BetterTTV Channel Emotes"));
                        messages.Add(new Message(words));
                    }
                }

                // bttv global emotes
                {
                    List<Word> words = new List<Word>();

                    foreach (var emote in Emotes.BttvGlobalEmotes.Values)
                    {
                        words.Add(new Word { Type = SpanType.Emote, Value = emote, Tooltip = emote.Tooltip, CopyText = emote.Name, Link = "@insertText|" + emote.Name + " " });
                    }

                    if (words.Count != 0)
                    {
                        messages.Add(new Message("BetterTTV Global Emotes"));
                        messages.Add(new Message(words));
                    }
                }

                // ffz channel emotes
                if (channel != null)
                {
                    List<Word> words = new List<Word>();

                    foreach (var emote in channel.FfzChannelEmotes.Values)
                    {
                        words.Add(new Word { Type = SpanType.Emote, Value = emote, Tooltip = emote.Tooltip, CopyText = emote.Name, Link = "@insertText|" + emote.Name + " " });
                    }

                    if (words.Count != 0)
                    {
                        messages.Add(new Message("FrankerFaceZ Channel Emotes"));
                        messages.Add(new Message(words));
                    }
                }

                // ffz global emotes
                {
                    List<Word> words = new List<Word>();

                    foreach (var emote in Emotes.FfzGlobalEmotes.Values)
                    {
                        words.Add(new Word { Type = SpanType.Emote, Value = emote, Tooltip = emote.Tooltip, CopyText = emote.Name, Link = "@insertText|" + emote.Name + " " });
                    }

                    if (words.Count != 0)
                    {
                        messages.Add(new Message("FrankerFaceZ Global Emotes"));
                        messages.Add(new Message(words));
                    }
                }

                _messages = messages.ToArray();

                scrollAtBottom = false;
                _scroll.Value = 0;
                updateMessageBounds();
                Invalidate();
            }
        }
    }
}
