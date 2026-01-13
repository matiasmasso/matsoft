Public Class Menu_Tel

    Private _Tel As DTOContactTel

    Public Event AfterUpdate(sender As Object, e As EventArgs)


    Public Sub New(oTel As DTOContactTel)
        MyBase.New()
        _Tel = oTel
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Truca(),
        MenuItem_Copy()
        })
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

    Private Function MenuItem_Truca() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        Select Case _Tel.Cod
            Case DTOContactTel.Cods.tel, DTOContactTel.Cods.movil
                With oMenuItem
                    .Text = "truca"
                    '.Image = IIf(MatCommunicator.enabled, My.Resources.tel, My.Resources.trucadaKO)
                    AddHandler .Click, AddressOf Do_Truca
                End With
            Case Else
                oMenuItem.Visible = False
        End Select

        Return oMenuItem
    End Function

    Private Function MenuItem_Copy() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_Copy
        Return oMenuItem
    End Function

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Tel(_Tel)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Copy(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(_Tel.Value, True)
    End Sub

    Private Sub Do_Truca(ByVal sender As Object, ByVal e As System.EventArgs)
        Cx3OutboundCall.MakeCall(_Tel.value)
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
