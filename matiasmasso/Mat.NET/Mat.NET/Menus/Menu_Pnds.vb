Public Class Menu_Pnds
    Private _Pnd As DTOPnd
    Private _Pnds As List(Of DTOPnd)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oPnd As DTOPnd)
        MyBase.New()
        _Pnd = oPnd
        _Pnds = New List(Of DTOPnd)
        _Pnds.Add(_Pnd)
    End Sub

    Public Sub New(ByVal oPnds As List(Of DTOPnd))
        MyBase.New()
        _Pnds = oPnds
        If _Pnds.Count > 0 Then
            _Pnd = oPnds(0)
        End If
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Rebut(), _
        MenuItem_Del(), _
        MenuItem_Extracte(), _
        MenuItem_Clon(), _
        MenuItem_Contact(), _
        MenuItem_Compensar()})
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

    Private Function MenuItem_Rebut() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Rebut"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_Rebut
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function

    Private Function MenuItem_Clon() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Clonar"
        oMenuItem.Image = My.Resources.tampon
        AddHandler oMenuItem.Click, AddressOf Do_Clon
        Return oMenuItem
    End Function

    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Extracte"
        'oMenuItem.Image = My.Resources.
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function

    Private Function MenuItem_Contact() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Contacte..."
        If _Pnd Is Nothing Then
            oMenuItem.Enabled = False
        Else
            If _Pnd.Contact Is Nothing Then
                oMenuItem.Enabled = False
            Else
                oMenuItem.DropDownItems.AddRange(New Menu_Contact(_Pnd.Contact).Range)
            End If
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Compensar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Compensar"
        oMenuItem.Visible = False
        If _Pnds.Count = 2 Then
            If (_Pnds(0).Amt.Eur < 0 And _Pnds(1).Amt.Eur > 0) _
            Or (_Pnds(1).Amt.Eur < 0 And _Pnds(0).Amt.Eur > 0) Then
                oMenuItem.Visible = True
            End If
        End If
        'oMenuItem.Image = My.Resources.
        AddHandler oMenuItem.Click, AddressOf Do_Compensa
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Function OldPnd(Optional Idx As Integer = 0) As Pnd
        Dim retval As New Pnd(_Pnds(Idx).Id)
        Return retval
    End Function

    Private Sub Do_Zoom()
        Dim oFrm As New Frm_Contact_Pnd(OldPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Rebut()
        root.ShowRebut(OldPnd.Rebut)
    End Sub

    Private Sub Do_Del()
        Dim rc As MsgBoxResult = MsgBox("eliminem la partida de " & _Pnd.Amt.CurFormatted & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        Select Case rc
            Case MsgBoxResult.Ok
                Dim exs As New List(Of Exception)
                If OldPnd.Delete(exs) Then
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                    MsgBox("Partida eliminada", MsgBoxStyle.Information, "MAT.NET")
                Else
                    MsgBox("error al eliminar la partida pendent" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            Case MsgBoxResult.Cancel
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End Select
    End Sub

    Private Sub Do_Extracte()
        Dim oContact As DTOContact = _Pnd.Contact
        Dim oCta As DTOPgcCta = _Pnd.Cta
        Dim oFrm As New Frm_Extracte(oContact, oCta)
        oFrm.Show()
    End Sub

    Private Sub Do_Clon()
        Dim oPnd As Pnd = OldPnd()
        oPnd.Clon()
        Dim oFrm As New Frm_Contact_Pnd(oPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Compensa()
        Dim oAbono As Pnd = Nothing
        Dim oCargo As Pnd = Nothing
        If _Pnds(0).Amt.Eur < 0 Then
            oAbono = OldPnd(0)
            oCargo = OldPnd(1)
        Else
            oAbono = OldPnd(1)
            oCargo = OldPnd(0)
        End If

        oCargo.FraNum = oCargo.FraNum & "," & oAbono.FraNum
        oCargo.Amt.Add(oAbono.Amt)
        Dim exs As New List(Of Exception)
        If Not oCargo.Update(exs) Then
            MsgBox("error al desar PND" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If
        oAbono.Status = Pnd.StatusCod.compensat
        exs = New List(Of Exception)
        If Not oAbono.Update(exs) Then
            MsgBox("error al desar PND" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)

    End Sub


    '==========================================================================
    '                               EVENT TRIGGERS
    '==========================================================================

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class


