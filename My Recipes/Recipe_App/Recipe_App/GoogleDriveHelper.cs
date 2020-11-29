using System;
using System.Collections.Generic;
using System.Text;

namespace Recipe_App
{
    public static class GoogleDriveHelper
    {

        public static void SaveToCloud()
        {
            //DriveClass.DriveApi.NewDriveContents(client).SetResultCallback(new System.Action<IDriveApiDriveContentsResult>((result) => {

            //    //Create MetadataChangeSet
            //    MetadataChangeSet metadataChangeSet = new MetadataChangeSet.Builder()
            //        .SetTitle("MyDbFile.db")
            //        .SetMimeType("application/x-sqlite3").Build();

            //    var contents = result.DriveContents;

            //    //push the bitmap data to DriveContents
            //    bitmapFromYourPicture.Compress(CompressFormat.Jpeg, 1, contents.OutputStream);
            //    IntentSender intentSender = DriveClass.DriveApi
            //            .NewCreateFileActivityBuilder()
            //            .SetInitialMetadata(metadataChangeSet)
            //            .SetInitialDriveContents(contents)
            //            .Build(client);

            //    StartIntentSenderForResult(intentSender, 2, null, 0, 0, 0);
            // }));

            // DriveService()

        }
    }
}
