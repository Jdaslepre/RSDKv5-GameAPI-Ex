using System;

namespace RSDK.API
{
#if RETRO_REV02
	public static class Auth
	{
		public static void ClearPrerollErrors() => APITable.ClearPrerollErrors();
		public static void TryAuth() => APITable.TryAuth();
		public static int GetUserAuthStatus() { return APITable.GetUserAuthStatus(); }
		public static bool32 GetUsername(RSDK.String* username) { return APITable.GetUsername(username); }
	}
#endif

	public static class Storage
	{
		// load user file from game dir
		public static bool32 LoadFile(char8* fileName, void* buffer, uint32 size) { return RSDKTable.LoadUserFile(fileName, buffer, size); }
		public static bool32 SaveFile(char8* fileName, void* buffer, uint32 size) { return RSDKTable.SaveUserFile(fileName, buffer, size); }

#if RETRO_REV02
		// load user file from user dir (e.g. cloud saves or etc)
		public static void LoadUserFile(char8* fileName, void* buffer, uint32 size, function void(int32 status) callback) => APITable.LoadUserFile(fileName, buffer, size, callback);
		public static void SaveUserFile(char8* fileName, void* buffer, uint32 size, function void(int32 status) callback, bool32 compressed) => APITable.SaveUserFile(fileName, buffer, size, callback, compressed);
		public static void DeleteUserFile(char8* fileName, function void(int32 status) callback) => APITable.DeleteUserFile(fileName, callback);

		// Storage
		public static void TryInitStorage() => APITable.TryInitStorage();
		public static int GetStorageStatus() { return APITable.GetStorageStatus(); }
		public static int GetSaveStatus() { return APITable.GetSaveStatus(); }
		public static void ClearSaveStatus() => APITable.ClearSaveStatus();
		public static void SetSaveStatusContinue() => APITable.SetSaveStatusContinue();
		public static void SetSaveStatusOK() => APITable.SetSaveStatusOK();
		public static void SetSaveStatusForbidden() => APITable.SetSaveStatusForbidden();
		public static void SetSaveStatusError() => APITable.SetSaveStatusError();
		public static void SetNoSave(bool32 noSave) => APITable.SetNoSave(noSave);
		public static bool32 GetNoSave() { return APITable.GetNoSave(); }

		public struct UserDB
		{
			public enum Types
			{
				DBVAR_UNKNOWN,
				DBVAR_BOOL,
				DBVAR_UINT8,
				DBVAR_UINT16,
				DBVAR_UINT32,
				DBVAR_UINT64, // unimplemented in RSDKv5
				DBVAR_INT8,
				DBVAR_INT16,
				DBVAR_INT32,
				DBVAR_INT64, // unimplemented in RSDKv5
				DBVAR_FLOAT,
				DBVAR_DOUBLE,  // unimplemented in RSDKv5
				DBVAR_VECTOR2, // unimplemented in RSDKv5
				DBVAR_VECTOR3, // unimplemented in RSDKv5
				DBVAR_VECTOR4, // unimplemented in RSDKv5
				DBVAR_COLOR,
				DBVAR_STRING,
				DBVAR_HASHMD5, // unimplemented in RSDKv5
			}

			public uint16 id;

			// Management
			public void Init() mut => id = (.)(-1);
			public void Init(char8* name, char8 *args, ...) mut
			{
				VarArgs vArgs = VarArgs();
				vArgs.Start!();
				id = APITable.InitUserDB(name, args, vArgs.ToVAList());
				vArgs.End!();
			}

			public void Load(char8* filename, function void(int32 status) callback) mut => id = APITable.LoadUserDB(filename, callback);
			public void Save(function void(int32 status) callback) => APITable.SaveUserDB(id, callback);
			public void Clear() => APITable.ClearUserDB(id);
			public static void ClearAll() => APITable.ClearAllUserDBs();

			public bool Loaded() { return id != (.)(-1); } 

			public bool Matches(RSDK.API.Storage.UserDB other) { return id == other.id; }
			public bool Matches(RSDK.API.Storage.UserDB* other)
			{
				if (other != null)
					return id == other.id;
				else
					return id == (.)(-1);
				
			}

			// Sorting
			public void SetupSorting() => APITable.SetupUserDBRowSorting(id);
			public void Sort(int32 type, char8* name, bool32 sortAscending) => APITable.SortDBRows(id, type, name, sortAscending);
			public bool32 RowsChanged() { return APITable.GetUserDBRowsChanged(id); }
			public void AddSortFilter(UserDB.Types type, char8* name, void* value) => APITable.AddRowSortFilter(id, (.)type, name, value);
			public int32 SortedRowCount() { return APITable.GetSortedUserDBRowCount(id); }
			public int32 GetSortedRowID(uint16 row) { return APITable.GetSortedUserDBRowID(id, row); }

			// Rows
			public int32 AddRow() { return APITable.AddUserDBRow(id); }
			public void RemoveRow(uint16 row) => APITable.RemoveDBRow(id, row);
			public void RemoveAllRows(uint16 row) => APITable.RemoveAllDBRows(id);
			public uint32 GetRowUUID(uint16 row) { return APITable.GetUserDBRowUUID(id, row); }
			public int32 GetRowByID(uint32 uuid) { return APITable.GetUserDBRowByID(id, uuid); }
			public void GetRowCreationTime(uint16 row, char8* buffer, uint bufferSize, char8* format) => APITable.GetUserDBRowCreationTime(id, row, buffer, bufferSize, format);

			// Values
			public void SetValue(int row, UserDB.Types type, char8* name, void* value) => APITable.SetUserDBValue(id, (.)row, (.)type, name, value);
			public void GetValue(int row, UserDB.Types type, char8* name, void* value) => APITable.GetUserDBValue(id, (.)row, (.)type, name, value);
		}
#endif
	}
}