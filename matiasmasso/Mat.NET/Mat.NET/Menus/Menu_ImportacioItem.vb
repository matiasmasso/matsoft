Public Class Menu_ImportacioItem

    Private _ImportacioItem As ImportacioItem

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToRemove(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oImportacioItem As ImportacioItem)
        MyBase.New()
        _ImportacioItem = oImportacioItem
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Import(), _
        MenuItem_CopyLink(), _
        MenuItem_Delete()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Import() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar Pdf"
        oMenuItem.Image = My.Resources.clip
        AddHandler oMenuItem.Click, AddressOf Do_Import
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retirar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As EventArgs)
        With _ImportacioItem
            Select Case .SrcCod
                Case ImportacioItem.SourceCodes.Alb
                    Dim oAlb As New Alb(.Guid)
                    root.ShowAlb(oAlb)
                Case ImportacioItem.SourceCodes.Fra
                    Dim oCca As New Cca(.Guid)
                    root.ShowCca(oCca)
                Case ImportacioItem.SourceCodes.Cmr
                    Dim oDocFile As DTODocFile = .DocFile
                    UIHelper.ShowStream(oDocFile)
                Case ImportacioItem.SourceCodes.FraTrp
                    Dim oCca As New Cca(.Guid)
                    root.ShowCca(oCca)
                Case Else
                    MsgBox("visor no implementat per aquest document")
            End Select
        End With
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim s As String = "Retirem el document d'aquesta importació?"
        Select Case _ImportacioItem.SrcCod
            Case ImportacioItem.SourceCodes.Fra, ImportacioItem.SourceCodes.FraTrp
                s = s & vbCrLf & "si es vol eliminar de comptabilitat cal fer-ho des de l'extracte del proveidor"
        End Select

        Dim rc As MsgBoxResult = MsgBox(s, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim retval As Boolean = False



            Select Case _ImportacioItem.SrcCod
                Case ImportacioItem.SourceCodes.Alb
                    If RetiraDoc(_ImportacioItem) Then
                        MsgBox("albará retirat d'aquesta importació" & vbCrLf & "si es vol eliminar l'albará del tot s'ha de fer per albarans", MsgBoxStyle.Information, "MAT.NET")
                        retval = True
                    Else
                        MsgBox("No s'ha trobat aquest albará a la importacio", MsgBoxStyle.Information, "MAT.NET")
                    End If

                Case ImportacioItem.SourceCodes.Fra
                    If RetiraDoc(_ImportacioItem) Then
                        MsgBox("factura retirada d'aquesta importació" & vbCrLf & "si es vol eliminar la factura del tot s'ha de fer per comptabilitat", MsgBoxStyle.Information, "MAT.NET")
                        retval = True
                    Else
                        MsgBox("No s'ha trobat la factura a la importacio", MsgBoxStyle.Information, "MAT.NET")
                    End If

                Case ImportacioItem.SourceCodes.Cmr
                    If RetiraDoc(_ImportacioItem) Then
                        Dim oBigFile As New BigFileSrc(DTODocFile.Cods.Cmr, _ImportacioItem.Guid)
                        oBigFile.Delete()
                        retval = True
                    Else
                        MsgBox("No s'ha trobat el CMR", MsgBoxStyle.Information, "MAT.NET")
                    End If


                Case ImportacioItem.SourceCodes.FraTrp
                    If RetiraDoc(_ImportacioItem) Then
                        MsgBox("factura retirada d'aquesta importació" & vbCrLf & "si es vol eliminar la factura del tot s'ha de fer per comptabilitat", MsgBoxStyle.Information, "MAT.NET")
                        retval = True
                    Else
                        MsgBox("No s'ha trobat la factura a la importacio", MsgBoxStyle.Information, "MAT.NET")
                    End If

                Case Else
                    MsgBox("funció encara no implementada per aquest document", MsgBoxStyle.Exclamation, "MAT.NET")
            End Select
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Function RetiraDoc(oItem As ImportacioItem) As Boolean
        Dim retval As Boolean = False
        Dim oHeader As Importacio = oItem.Parent
        oHeader.Items.Remove(oItem)
        Dim exs as New List(Of exception)
        If oHeader.Update( exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oItem))
            retval = True
        Else
            MsgBox("error al eliminar el document de la remesa" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

        Return retval
    End Function

    Private Sub Do_Import(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub


    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_ImportacioItem.DocFile)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class

