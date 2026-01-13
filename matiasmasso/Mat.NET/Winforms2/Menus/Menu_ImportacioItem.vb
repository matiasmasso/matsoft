Public Class Menu_ImportacioItem

    Private _ImportacioItem As DTOImportacioItem

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToRemove(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oImportacioItem As DTOImportacioItem)
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

    Private Async Sub Do_Zoom(ByVal sender As Object, ByVal e As EventArgs)
        Dim exs As New List(Of Exception)
        With _ImportacioItem
            Select Case .SrcCod
                Case DTOImportacioItem.SourceCodes.alb
                    Dim oDelivery = Await FEB.Delivery.Find(.Guid, exs)
                    If exs.Count = 0 Then
                        Dim oContact As DTOContact = oDelivery.contact
                        If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oContact, DTOAlbBloqueig.Codis.ALB, exs) Then
                            Dim oFrm As New Frm_AlbNew2(oDelivery)
                            oFrm.Show()
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case DTOImportacioItem.SourceCodes.Fra, DTOImportacioItem.SourceCodes.FraTrp
                    Dim oCca As New DTOCca(.Guid)
                    Dim oFrm As New Frm_Cca(oCca)
                    oFrm.Show()
                Case Else
                    Dim oFrm As New Frm_ImportacioItem(.Parent, .DocFile)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()

                    'Dim oDocFile As DTODocFile = .DocFile
                    'If Not Await UIHelper.ShowStreamAsync(exs, oDocFile) Then
                    ' UIHelper.WarnError(exs)
                    'End If
            End Select
        End With
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim s As String = "Retirem el document d'aquesta importació?"
        Select Case _ImportacioItem.SrcCod
            Case DTOImportacioItem.SourceCodes.Fra, DTOImportacioItem.SourceCodes.FraTrp
                s = s & vbCrLf & "si es vol eliminar de comptabilitat cal fer-ho des de l'extracte del proveidor"
        End Select

        Dim rc As MsgBoxResult = MsgBox(s, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim retval As Boolean = False



            Select Case _ImportacioItem.SrcCod
                Case DTOImportacioItem.SourceCodes.Alb
                    If Await RetiraDoc(_ImportacioItem) Then
                        MsgBox("albará retirat d'aquesta importació" & vbCrLf & "si es vol eliminar l'albará del tot s'ha de fer per albarans", MsgBoxStyle.Information, "MAT.NET")
                        retval = True
                    Else
                        MsgBox("No s'ha trobat aquest albará a la importacio", MsgBoxStyle.Information, "MAT.NET")
                    End If

                Case DTOImportacioItem.SourceCodes.Fra
                    If Await RetiraDoc(_ImportacioItem) Then
                        MsgBox("factura retirada d'aquesta importació" & vbCrLf & "si es vol eliminar la factura del tot s'ha de fer per comptabilitat", MsgBoxStyle.Information, "MAT.NET")
                        retval = True
                    Else
                        MsgBox("No s'ha trobat la factura a la importacio", MsgBoxStyle.Information, "MAT.NET")
                    End If

                Case DTOImportacioItem.SourceCodes.Cmr, DTOImportacioItem.SourceCodes.PackingList, DTOImportacioItem.SourceCodes.Proforma
                    If Await RetiraDoc(_ImportacioItem) Then
                        retval = True
                    Else
                        MsgBox("No s'ha trobat el CMR", MsgBoxStyle.Information, "MAT.NET")
                    End If


                Case DTOImportacioItem.SourceCodes.FraTrp
                    If Await RetiraDoc(_ImportacioItem) Then
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

    Private Async Function RetiraDoc(oItem As DTOImportacioItem) As Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim retval As Boolean = False

        Dim oHeader As DTOImportacio = oItem.Parent
        If oHeader Is Nothing Then
            MsgBox("Error. falten dades per eliminar aquesta partida")
        Else
            If FEB.Importacio.Load(exs, oHeader) Then
                oHeader.items.Remove(oItem)
                Dim id = Await FEB.Importacio.Update(oHeader, exs)
                If exs.Count = 0 Then
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(oItem))
                    retval = True
                Else
                    MsgBox("error al eliminar el document de la remesa" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
                End If
            Else
                UIHelper.WarnError(exs)
            End If

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

