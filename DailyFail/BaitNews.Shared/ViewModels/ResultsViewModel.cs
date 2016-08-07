using System;
using System.Collections.Generic;
using System.Linq;

namespace BaitNews
{
	public class ResultsViewModel
	{
		public IEnumerable<Answer> Answers { get; }
		public ResultsViewModel(IEnumerable<Answer> answers)
		{
			Answers = answers;
		}

		public int WrongCount => Answers.Count(x => !x.CorrectAnswer);
		public int CorrectCount => Answers.Count(x => x.CorrectAnswer);
	}
}

