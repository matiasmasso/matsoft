Public Class BancController
    Inherits _BaseController

    <HttpPost>
    <Route("api/bancs")>
    Public Function bancs(user As DTOGuidNom) As List(Of DUI.Banc)
        Dim retval As New List(Of DUI.Banc)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim oBancSdos As List(Of DTOBancSdo) = BLL.BLLBancSdos.Last()
        For Each oBancSdo As DTOBancSdo In oBancSdos
            Dim item As New DUI.Banc
            With item
                .Guid = oBancSdo.Banc.Guid.ToString
                .Nom = BLLBanc.AbrOrNom(oBancSdo.Banc)
                .Iban = BLLBancSdo.IbanDigits(oBancSdo)
                .Saldo = BLLAmt.Eur(oBancSdo.Saldo())
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/bancs/sprite")>
    Public Function sprite(data As DUI.Sprite) As System.Net.Http.HttpResponseMessage
        Dim exs As New List(Of Exception)

        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim sJsonRequest As String = jss.Serialize(data)
        Dim sHash As String = CryptoHelper.GetSha256Hash(sJsonRequest)

        Dim oSprite As DTOSprite = BLLSprite.Find(sHash)
        If oSprite Is Nothing Then
            Dim oBancs As New List(Of DTOBanc)
            For Each oGuid As Guid In data.guids
                oBancs.Add(New DTOBanc(oGuid))
            Next
            oSprite = BLLSprite.Factory(oBancs, data.itemWidth, sHash, exs)
        End If

        Dim oBytes() As Byte = BLL.ImageHelper.GetByteArrayFromImg(oSprite.Image)
        Dim MS As New System.IO.MemoryStream(oBytes)
        Dim retval As New System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
        With retval
            .Content = New System.Net.Http.StreamContent(MS)
            .Content.Headers.ContentType = New System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg")
        End With
        Return retval
    End Function

End Class


