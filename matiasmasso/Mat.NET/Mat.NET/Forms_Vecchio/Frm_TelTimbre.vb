


Public Class Frm_TelTimbre
    Private mTimbre As TelTimbre
    Private mAllowEvents As Boolean = False
    Private mWav As Byte() = Nothing
    Private mDirtyWav As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oObject As Object)
        MyBase.new()
        Me.InitializeComponent()
        mTimbre = oObject
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mTimbre
            TextBoxNom.Text = .Nom
            ButtonPlay.Enabled = .AudioStream IsNot Nothing
            If .exists Then
                ButtonDel.Enabled = .allowdelete
            End If
        End With
    End Sub

    Private Sub ButtonBrowse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonBrowse.Click
        Dim sFilename As String = BrowseFile(TextBoxSearchEsp.Text)
        If sFilename > "" Then
            TextBoxSearchEsp.Text = sFilename
            ButtonUpload.Enabled = True
        End If
    End Sub

    Private Function BrowseFile(Optional ByVal sInitialDirectory As String = "") As String
        Dim retVal As String = ""
        Dim oDlg As New OpenFileDialog
        With oDlg
            .InitialDirectory = sInitialDirectory
            .Title = "Buscar grabació de veu"
            .Filter = "audio (.wma)|*.wma|tots els arxius|*.*"
            If .ShowDialog() = DialogResult.OK Then
                retVal = .FileName
            End If
        End With
        Return retVal
    End Function

    Private Sub ButtonUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUpload.Click
        mWav = GetWavStreamFromFile(TextBoxSearchEsp.Text)
        TextBoxSearchEsp.Text = ""
        ButtonUpload.Enabled = False
        ButtonPlay.Enabled = True
        mDirtyWav = True
        SetDirty()
    End Sub

    Shared Function GetWavStreamFromFile(ByVal sFileName As String) As Byte()
        Dim oFileStream As New System.IO.FileStream(sFileName, System.IO.FileMode.Open)
        Dim oStream(CInt(oFileStream.Length - 1)) As Byte
        oFileStream.Read(oStream, 0, CInt(oFileStream.Length))
        oFileStream.Close()
        Return oStream
    End Function

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged
        SetDirty()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mTimbre
            .Nom = TextBoxNom.Text
            .AudioStream = mWav
            .Update()
            RaiseEvent AfterUpdate(mTimbre, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mTimbre.allowDelete Then
            mTimbre.delete()
        End If
    End Sub

    Private Sub SetDirty()
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonPlay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPlay.Click
  
    End Sub
End Class