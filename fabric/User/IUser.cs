using System.Collections.Generic;

namespace fabricsdk.fabric.User 
{
	/**
		IUser - Should be implemented by the embedding application.
	*/
	public interface IUser
	{
		string Name {get;}
		
		HashSet<string> Roles {get;}

		string Account {get;}

		string Affiliation {get;}

		IEnrollment Enrollment {get;}

		string MspID {get;}
	}
}