using System;

namespace fabricsdk.fabric.User
{
	public static class UserValidator
	{
		public static  void Check(IUser user)
		{
			if (user == null)
				throw new ArgumentException("IUser is null.");			
			
			if (string.IsNullOrEmpty(user.Name))
				throw new ArgumentException("IUser's name is missing.");

			if (user.Enrollment == null)
				throw new ArgumentException($"IUser {user.Name} has no enrollment set.");

			if (string.IsNullOrEmpty(user.MspID))
				throw new ArgumentException($"IUser {user.Name}'s MSPID is missing.");

			if (string.IsNullOrEmpty(user.Enrollment.Certificate))
				throw new ArgumentException($"IUser {user.Name}'s enrollment is missing a certificate.");

			if (user.Enrollment.Key == null)
				throw new ArgumentException($"IUser {user.Name}'s enrollment is missing a signing key.");
		}
	}
}