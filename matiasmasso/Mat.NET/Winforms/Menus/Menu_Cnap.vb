Public Class Menu_Cnap

    Private _Cnap As DTOCnap

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oCnap As DTOCnap)
        MyBase.New()
        _Cnap = oCnap
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_SellOut(),
        MenuItem_Export(),
        MenuItem_Delete()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Sellout() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sellout"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Sellout
        Return oMenuItem
    End Function

    Private Function MenuItem_Export() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Exportar"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Export
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cnap(_Cnap)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Export()
        Dim oDlg As New SaveFileDialog
        With oDlg
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            .RestoreDirectory = True

            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim oFileStream As New System.IO.FileStream(.FileName, System.IO.FileMode.Append, System.IO.FileAccess.Write)
                Dim oStreamWriter As New System.IO.StreamWriter(oFileStream)
                'Dim oDs As DataSet =Current.session.emp.GetCatalegForEshops
                'Dim oTb As DataTable = oDs.Tables(0)
                'For Each orow As DataRow In oTb.Rows
                ' For iCol As Integer = 0 To oTb.Columns.Count - 1
                ' If iCol > 0 Then oStreamWriter.Write(";")
                ' Dim sVal As String = orow(iCol).ToString
                ' If sVal.IndexOfAny(New Char() {";"c, Chr(10), Chr(13)}) >= 0 Then
                ' oStreamWriter.Write(Chr(34).ToString & orow(iCol) & Chr(34).ToString())
                'Else
                '    oStreamWriter.Write(orow(iCol))
                'End If
                '    Next
                '    oStreamWriter.WriteLine()
                '    Next
                '    oStreamWriter.Close()
                '    MsgBox("fitxer grabat", MsgBoxStyle.Information, "MAT.NET")
            End If
        End With

    End Sub

    Private Async Sub Do_Sellout(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB2.SellOut.Factory(exs, Current.Session.User, , DTOSellOut.ConceptTypes.Product, DTOSellOut.Formats.Units)
        If exs.Count = 0 Then
            'oSellout.AddFilter(DTOSellOut.Filter.Cods.CNap, {_Cnap})
            'Dim oFrm As New Frm_SellOut(oSellout)
            'oFrm.Show()
            MsgBox("(sorry, pendent de implementar)")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Del()
        Dim rc As MsgBoxResult = MsgBox("eliminem " & _Cnap.FullNom(Current.Session.Lang) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Cnap.Delete(_Cnap, exs) Then
                RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el codi")
            End If
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


