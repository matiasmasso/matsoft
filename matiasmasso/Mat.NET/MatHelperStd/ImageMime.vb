Public Class ImageMime
    Public Property Mime As MimeCods
    Public Property ByteArray As Byte()


    Public Shared Function Factory(Optional ByVal byteArray As Byte() = Nothing, Optional oMime As MatHelperStd.MimeCods = MimeCods.NotSet) As ImageMime
        Dim retval As ImageMime = New ImageMime()
        retval.ByteArray = If(byteArray, New Byte() {})
        retval.Mime = If(oMime = MimeCods.NotSet, MimeCods.Jpg, oMime)
        Return retval
    End Function

End Class

