

Public Class Menu_Pnd
    Private _Pnd As DTOPnd
    Private _Pnds As List(Of DTOPnd)
    Private mPnd As Pnd
    Private mPnds As Pnds

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oPnd As Pnd)
        MyBase.New()
        mPnd = oPnd
        mPnds = New Pnds
        mPnds.Add(mPnd)

        _Pnd = New DTOPnd(oPnd.Guid)
        _Pnds = New List(Of DTOPnd)
        _Pnds.Add(_Pnd)
    End Sub

    Public Sub New(ByVal oPnds As Pnds)
        MyBase.New()
        mPnds = oPnds
        mPnd = oPnds(0)
        _Pnds = New List(Of DTOPnd)
        For Each oPnd As Pnd In mPnds
            _Pnds.Add(New DTOPnd(oPnd.Guid))
        Next
        _Pnd = _Pnds(0)
    End Sub

    Public Sub New(oPnd As DTOPnd)
        MyBase.New()
        _Pnd = oPnd
        _Pnds = New List(Of DTOPnd)
        _Pnds.Add(_Pnd)

        mPnd = New Pnd
        mPnd.Guid = _Pnd.Guid
        mPnds = New Pnds
        mPnds.Add(mPnd)
    End Sub

    Public Sub New(ByVal oPnds As List(Of DTOPnd))
        MyBase.New()
        _Pnds = oPnds
        _Pnd = _Pnds(0)

        mPnds = New Pnds
        For Each oPnd As DTOPnd In _Pnds
            Dim item As New Pnd
            item.Guid = oPnd.Guid
            mPnds.Add(item)
        Next
        mPnd = mPnds(0)
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
        If mPnd.Contact Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.DropDownItems.AddRange(New Menu_Contact(mPnd.Contact).Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Compensar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Compensar"
        oMenuItem.Visible = False
        If mPnds.Count = 2 Then
            If (mPnds(0).Amt.Eur < 0 And mPnds(1).Amt.Eur > 0) _
            Or (mPnds(1).Amt.Eur < 0 And mPnds(0).Amt.Eur > 0) Then
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
        Dim oFrm As New Frm_Contact_Pnd(mPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Rebut(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowRebut(mPnd.Rebut)
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("eliminem la partida de " & mPnd.Amt.CurFormat & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        Select Case rc
            Case MsgBoxResult.Ok
                Dim exs As New List(Of Exception)
                If mPnd.Delete(exs) Then
                    MsgBox("Partida eliminada", MsgBoxStyle.Information, "MAT.NET")
                    RaiseEvent AfterUpdate(sender, e)
                Else
                    MsgBox("error al eliminar la partida pendent" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            Case MsgBoxResult.Cancel
                MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End Select
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContact As Contact = mPnd.Contact
        Dim oCta As PgcCta = mPnd.Cta
        Dim oFrm As New Frm_CliCtas(oContact, oCta)
        oFrm.Show()
    End Sub

    Private Sub Do_Clon(ByVal sender As Object, ByVal e As System.EventArgs)
        mPnd.Clon()
        Dim oFrm As New Frm_Contact_Pnd(mPnd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Compensa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oAbono As Pnd = Nothing
        Dim oCargo As Pnd = Nothing
        If mPnds(0).Amt.Eur < 0 Then
            oAbono = mPnds(0)
            oCargo = mPnds(1)
        Else
            oAbono = mPnds(1)
            oCargo = mPnds(0)
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
        RaiseEvent AfterUpdate(sender, e)

    End Sub


    '==========================================================================
    '                               EVENT TRIGGERS
    '==========================================================================

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class

