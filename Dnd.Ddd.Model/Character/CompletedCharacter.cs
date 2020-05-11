using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character
{
    public class CompletedCharacter : Character
    {
        protected CompletedCharacter()
        {
        }

        private CompletedCharacter(CharacterDraft characterDraft)
        {
            Name = characterDraft.Name;
            PlayerId = characterDraft.PlayerId;
            Strength = characterDraft.Strength;
            Dexterity = characterDraft.Dexterity;
            Constitution = characterDraft.Constitution;
            Wisdom = characterDraft.Wisdom;
            Intelligence = characterDraft.Intelligence;
            Charisma = characterDraft.Charisma;
            Race = characterDraft.Race;
            UiD = characterDraft.UiD;
        }

        public static CompletedCharacter FromDraft(CharacterDraft draft)
        {
            Guard.With<ArgumentNullException>().Against(draft.Strength == null, nameof(Strength));
            Guard.With<ArgumentNullException>().Against(draft.Dexterity == null, nameof(Dexterity));
            Guard.With<ArgumentNullException>().Against(draft.Constitution == null, nameof(Constitution));
            Guard.With<ArgumentNullException>().Against(draft.Wisdom == null, nameof(Wisdom));
            Guard.With<ArgumentNullException>().Against(draft.Intelligence == null, nameof(Intelligence));
            Guard.With<ArgumentNullException>().Against(draft.Charisma == null, nameof(Charisma));
            Guard.With<ArgumentNullException>().Against(draft.Name == null, nameof(Name));
            Guard.With<ArgumentNullException>().Against(draft.Race == null, nameof(Race));

            return new CompletedCharacter(draft);
        }
    }
}