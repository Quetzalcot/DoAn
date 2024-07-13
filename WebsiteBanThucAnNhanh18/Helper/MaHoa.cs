using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using System.Text;

namespace WebsiteBanThucAnNhanh18.Helper
{
	public class MaHoa
	{
		public static string GenerateRandomKey(int length = 5)
		{
			var pattern = @"qazwsxedcrfvtgbyhnujmikolpQWASZXCFRVBGTYHNMJUKIOL!@#$%^&*()__+";
			var sb = new StringBuilder();
			var ran = new Random();
			for (int i = 0; i < length; i++)
			{

				sb.Append(pattern[ran.Next(0, pattern.Length)]);
			}
			return sb.ToString();
		}
	}
}
