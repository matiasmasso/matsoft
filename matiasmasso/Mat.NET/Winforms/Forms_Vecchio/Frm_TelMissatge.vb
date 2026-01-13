
Imports System.Media
Imports System.IO

Public Class Frm_TelMissatge
    Private mMissatge As TelMissatge
    Private mAllowEvents As Boolean = False

    Private mDirtyWavEsp As Boolean = False
    Private mDirtyWavCat As Boolean = False
    Private mDirtyWavEng As Boolean = False

    Private mWavEsp() As Byte = Nothing
    Private mWavCat() As Byte = Nothing
    Private mWavEng() As Byte = Nothing

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oMissatge As TelMissatge)
        MyBase.new()
        Me.InitializeComponent()
        mMissatge = oMissatge
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mMissatge
            If .Exists Then
                TextBoxNom.Text = .Nom
                TextBoxEsp.Text = .TxtEsp
                TextBoxCat.Text = .TxtCat
                TextBoxEng.Text = .TxtEng
                ButtonPlayEsp.Enabled = .WavEsp IsNot Nothing
                ButtonPlayCat.Enabled = .WavCat IsNot Nothing
                ButtonPlayEng.Enabled = .WavEng IsNot Nothing
                CheckBoxObsoleto.Checked = .Obsoleto
                ButtonDel.Enabled = .AllowDelete
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged, _
         TextBoxEsp.TextChanged, _
         TextBoxCat.TextChanged, _
          TextBoxEng.TextChanged, _
           CheckBoxObsoleto.CheckedChanged

        SetDirty()
    End Sub

    Private Sub SetDirty()
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mMissatge
            .Nom = TextBoxNom.Text
            .TxtEsp = TextBoxEsp.Text
            .TxtCat = TextBoxCat.Text
            .TxtEng = TextBoxEng.Text

            If mDirtyWavEsp Then
                .WavEsp = mWavEsp
            End If
            If mDirtyWavCat Then
                .WavCat = mWavCat
            End If
            If mDirtyWavEng Then
                .WavEng = mWavEng
            End If

            .Update()
            RaiseEvent AfterUpdate(mMissatge, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mMissatge.AllowDelete Then
            mMissatge.delete()
        End If
    End Sub

    Private Sub ButtonUploadEsp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadEsp.Click
        mWavEsp = GetWavStreamFromFile(TextBoxSearchEsp.Text)
        TextBoxSearchEsp.Text = ""
        ButtonUploadEsp.Enabled = False
        ButtonPlayEsp.Enabled = True
        mDirtyWavEsp = True
        SetDirty()
    End Sub

    Private Sub ButtonUploadCat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadCat.Click
        mWavCat = GetWavStreamFromFile(TextBoxSearchCat.Text)
        TextBoxSearchCat.Text = ""
        ButtonUploadCat.Enabled = False
        ButtonPlayCat.Enabled = True
        mDirtyWavCat = True
        SetDirty()
    End Sub

    Private Sub ButtonUploadEng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUploadEng.Click
        mWavEng = GetWavStreamFromFile(TextBoxSearchEng.Text)
        TextBoxSearchEng.Text = ""
        ButtonUploadEng.Enabled = False
        ButtonPlayEng.Enabled = True
        mDirtyWavEng = True
        SetDirty()
    End Sub

    Shared Function GetWavStreamFromFile(ByVal sFileName As String) As Byte()
        Dim oFileStream As New System.IO.FileStream(sFileName, System.IO.FileMode.Open)
        Dim oStream(CInt(oFileStream.Length - 1)) As Byte
        oFileStream.Read(oStream, 0, CInt(oFileStream.Length))
        Return oStream
    End Function

    Private Sub ButtonBrowseEsp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrowseEsp.Click
        Dim sFilename As String = BrowseFile(TextBoxSearchEsp.Text)
        If sFilename > "" Then
            TextBoxSearchEsp.Text = sFilename
            ButtonUploadEsp.Enabled = True
        End If
    End Sub

    Private Sub ButtonBrowseCat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrowseCat.Click
        Dim sFilename As String = BrowseFile(TextBoxSearchCat.Text)
        If sFilename > "" Then
            TextBoxSearchCat.Text = sFilename
            ButtonUploadCat.Enabled = True
        End If
    End Sub

    Private Sub ButtonBrowseEng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrowseEng.Click
        Dim sFilename As String = BrowseFile(TextBoxSearchEng.Text)
        If sFilename > "" Then
            TextBoxSearchEng.Text = sFilename
            ButtonUploadEng.Enabled = True
        End If
    End Sub

    Private Function BrowseFile(Optional ByVal sInitialDirectory As String = "") As String
        Dim retVal As String = ""
        Dim oDlg As New OpenFileDialog
        With oDlg
            .InitialDirectory = sInitialDirectory
            .Title = "Buscar grabació de veu"
            .Filter = "audio (.wav)|*.wav|tots els arxius|*.*"
            If .ShowDialog() = DialogResult.OK Then
                retVal = .FileName
            End If
        End With
        Return retVal
    End Function

    Private Sub ButtonPlayEsp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPlayEsp.Click
        PlayWav(BLL.BLLLang.ESP)
    End Sub

    Private Sub ButtonPlayCat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPlayCat.Click
        PlayWav(BLL.BLLLang.CAT)
    End Sub

    Private Sub ButtonPlayEng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPlayEng.Click
        PlayWav(BLL.BLLLang.ENG)
    End Sub

    Private Sub PlayWav(ByVal oLang As DTOLang)
        Dim oArray As Byte()
        Select Case oLang.Id
            Case DTOLang.Ids.Cat
                oArray = IIf(mDirtyWavCat, mWavCat, mMissatge.WavCat)
            Case DTOLang.Ids.Eng
                oArray = IIf(mDirtyWavEng, mWavEng, mMissatge.WavEng)
            Case Else
                oArray = IIf(mDirtyWavEsp, mWavEsp, mMissatge.WavEsp)
        End Select

        Dim oMemStream As New MemoryStream(oArray)
        Dim oPlayer As New SoundPlayer
        oPlayer.Stream = oMemStream
        oPlayer.Play()
    End Sub
End Class