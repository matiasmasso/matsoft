

Public Class Menu_Pnd
    Private _Pnd As DTOPnd
    Private _Pnds As List(Of DTOPnd)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Sub New(oPnd As DTOPnd)
        MyBase.New()
        _Pnd = oPnd
        _Pnds = New List(Of DTOPnd)
        _Pnds.Add(_Pnd)
    End Sub

    Public Sub New(ByVal oPnds As List(Of DTOPnd))
        MyBase.New()
        _Pnds = oPnds
        _Pnd = _Pnds.First
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
        Dim exs As New List(Of Exception)
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Contacte..."
        If _Pnd.Contact Is Nothing Then
            oMenuItem.Enabled = False
        Else
            Dim oContactMenu = FEB.ContactMenu.FindSync(exs, _Pnd.Contact)
            oMenuItem.DropDownItems.AddRange(New Menu_Contact(_Pnd.Contact, oContactMenu).Range)
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

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If _Pnd.Csb Is Nothing Then
            Dim oFrm As New Frm_Contact_Pnd(_Pnd)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            If FEB.Csb.Load(_Pnd.Csb, exs) Then
                Dim oFrm As New Frm_Csb(_Pnd.Csb)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Do_Rebut(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oRebut = Await FEB.Pnd.Rebut(exs, _Pnd)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Rebut(oRebut)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("eliminem la partida de " & DTOAmt.CurFormatted(_Pnd.Amt) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        Select Case rc
            Case MsgBoxResult.Ok
                Dim exs As New List(Of Exception)
                If Await FEB.Pnd.Delete(_Pnd, exs) Then
                    MsgBox("Partida eliminada", MsgBoxStyle.Information, "MAT.NET")
                    RaiseEvent AfterUpdate(sender, e)
                Else
                    UIHelper.WarnError(exs, "error al eliminar la partida pendent")
                End If
            Case MsgBoxResult.Cancel
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End Select
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContact As DTOContact = _Pnd.Contact
        Dim oCta As DTOPgcCta = _Pnd.Cta
        Dim oFrm As New Frm_Extracte(oContact, oCta)
        oFrm.Show()
    End Sub

    Private Sub Do_Clon(ByVal sender As Object, ByVal e As System.EventArgs)
        _Pnd.Clon()
        Dim oFrm As New Frm_Contact_Pnd(_Pnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Compensa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oAbono As DTOPnd = Nothing
        Dim oCargo As DTOPnd = Nothing
        If _Pnds(0).Amt.Eur < 0 Then
            oAbono = _Pnds(0)
            oCargo = _Pnds(1)
        Else
            oAbono = _Pnds(1)
            oCargo = _Pnds(0)
        End If

        oCargo.FraNum = oCargo.FraNum & "," & oAbono.FraNum
        oCargo.Amt.Add(oAbono.Amt)
        Dim exs As New List(Of Exception)
        If Await FEB.Pnd.Update(oCargo, exs) Then
            oAbono.Status = DTOPnd.StatusCod.compensat
            If Not Await FEB.Pnd.Update(oAbono, exs) Then
                MsgBox(exs, "error al desar PND")
            End If
            RaiseEvent AfterUpdate(sender, e)
        Else
            MsgBox(exs, "error al desar PND")
        End If

    End Sub


    '==========================================================================
    '                               EVENT TRIGGERS
    '==========================================================================

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class

