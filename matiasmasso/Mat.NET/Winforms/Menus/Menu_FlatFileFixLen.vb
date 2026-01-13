

Public Class Menu_FlatFileFixLen
    Private mFlatFileFixLen As FlatFileFixLen

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oFlatFileFixLen As FlatFileFixLen)
        MyBase.New()
        mFlatFileFixLen = oFlatFileFixLen
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
        MenuItem_Import()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        oMenuItem.Text = "Zoom"
        Return oMenuItem
    End Function

    Private Function MenuItem_Import() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Import
        oMenuItem.Text = "Importar"
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_FlatFileFixLenType(mFlatFileFixLen)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.show()
    End Sub

    Private Sub Do_Import(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar fitxer " & mFlatFileFixLen.Nom
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                mFlatFileFixLen.Load(.FileName)
                Dim oFrm As New Frm_FlatFileFixLen(mFlatFileFixLen)
                oFrm.Show()
            End If
        End With
    End Sub

 
    '=======================================================================


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class

