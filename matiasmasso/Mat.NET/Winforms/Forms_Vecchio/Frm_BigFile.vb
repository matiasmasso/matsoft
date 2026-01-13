

Public Class Frm_BigFile
    Private mBigFile As BigFileSrc
    Private mAllowEvents As Boolean = False
    Private mDirtyKeys As Boolean = False

    Public Event AfterUpdate(ByVal sender As BigFileSrc, ByVal e As System.EventArgs)

    Public Sub New(ByVal oBigFile As BigFileSrc)
        MyBase.new()
        Me.InitializeComponent()
        mBigFile = oBigFile
        Me.Text = "BIGFILE " & mBigFile.Guid.ToString
        LoadSrcs(oBigFile.Src)
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        Xl_BigFile1.BigFile = mBigFile.BigFile
        With mBigFile
            If .BigFile IsNot Nothing Then
                TextBoxSrcGuid.Text = .Guid.ToString
                TextBoxStreamGuid.Text = .BigFile.Guid.ToString
                TextBoxFch.Text = .BigFile.FchCreated.ToString
                If .Exists Then
                    ButtonDel.Enabled = .isOrfe
                End If
            End If
        End With
    End Sub

    Private Sub LoadSrcs(ByVal oDefaultSrc As DTODocFile.Cods)
        Dim oItm As ListItem = Nothing
        For Each v As DTODocFile.Cods In [Enum].GetValues(GetType(DTODocFile.Cods))
            oItm = New ListItem(CShort(v), v.ToString)
            Dim idx As Integer = ComboBoxSrc.Items.Add(oItm)
            If v = oDefaultSrc Then ComboBoxSrc.SelectedIndex = idx
        Next
    End Sub

    Private Sub KeysChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxSrcGuid.TextChanged, _
         ComboBoxSrc.SelectedValueChanged

        If mAllowEvents Then
            mDirtyKeys = True
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_BigFile1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_BigFile1.AfterUpdate
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oSrcGuid As Guid = GuidHelper.GetGuid(TextBoxSrcGuid.Text)
        Dim oSrc As DTODocFile.Cods = CType(ComboBoxSrc.SelectedItem.Value, DTODocFile.Cods)
        Dim oBigfile As BigFileNew = Xl_BigFile1.BigFile

        If mDirtyKeys Then
            mBigFile.Delete()
        End If

        mBigFile = New BigFileSrc(oSrc, oSrcGuid, oBigfile)
        mBigFile.Update()
        RaiseEvent AfterUpdate(mBigFile, System.EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mBigFile.isOrfe Then
            mBigFile.Delete()
            Me.Close()
        End If
    End Sub
End Class