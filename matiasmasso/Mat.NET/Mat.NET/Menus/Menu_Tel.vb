Public Class Menu_Tel

    Private mTel As Tel

    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Public Sub New(oTel As Tel)
        MyBase.New()
        mTel = oTel
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Copy(), _
        MenuItem_Del() _
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
        Select Case mTel.Cod
            Case Tel.Cods.tel, Tel.Cods.movil
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

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Tel(mTel)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Copy(ByVal sender As Object, ByVal e As System.EventArgs)
        Clipboard.SetDataObject(mTel.Num, True)
    End Sub

    Private Sub Do_Truca(ByVal sender As Object, ByVal e As System.EventArgs)
        ' MatCommunicator.Truca(mTel.formatted)
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("eliminem el " & mTel.Cod.ToString & " " & mTel.Num & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mTel.Delete( exs) Then
                RefreshRequest(sender, e)
            Else
                MsgBox("error al eliminar telefon " & mTel.Num & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End If
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
