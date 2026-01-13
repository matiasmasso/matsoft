

Public Class Menu_Impagat
    Inherits Menu_Base

    Private _Impagats As List(Of DTOImpagat)


    Public Sub New(oImpagat As DTOImpagat)
        MyBase.New()
        _Impagats = New List(Of DTOImpagat)
        _Impagats.Add(oImpagat)
    End Sub


    Public Sub New(ByVal oImpagats As List(Of DTOImpagat))
        MyBase.New()
        _Impagats = oImpagats
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Extracte(),
        MenuItem_Contact(),
        MenuItem_Rebut(),
        MenuItem_Cobrar(),
        MenuItem_MailImpagat(),
        MenuItem_CopyTpv()
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

    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Extracte"
        'oMenuItem.Image = My.Resources.
        oMenuItem.Image = My.Resources.notepad
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function

    Private Function MenuItem_Contact() As ToolStripMenuItem
        Dim exs As New List(Of Exception)
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Contacte..."

        If _Impagats.Count > 0 Then
            Dim oImpagat As DTOImpagat = _Impagats(0)
            If oImpagat.Csb.Contact Is Nothing Then
                oMenuItem.Enabled = False
            Else
                Dim oContactMenu = FEB.ContactMenu.FindSync(exs, oImpagat.Csb.Contact)
                oMenuItem.DropDownItems.AddRange(New Menu_Contact(oImpagat.Csb.Contact, oContactMenu).Range)
            End If
        End If

        oMenuItem.Image = My.Resources.People_Blue
        Return oMenuItem
    End Function

    Private Function MenuItem_MailImpagat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Email impagat"
        'oMenuItem.Enabled = _Csb.Impagat
        oMenuItem.Image = My.Resources.MailSobreGroc
        AddHandler oMenuItem.Click, AddressOf Do_MailImpagat
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyTpv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "enllaç a Tpv"
        'oMenuItem.Enabled = _Csb.Impagat
        'oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_CopyTpv
        Return oMenuItem
    End Function

    Private Function MenuItem_Rebut() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Rebut"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_Rebut
        Return oMenuItem
    End Function

    Private Function MenuItem_Cobrar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Cobrar"
        oMenuItem.Image = My.Resources.SquareArrowTurquesa
        AddHandler oMenuItem.Click, AddressOf Do_Cobrar
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Impagat(_Impagats.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Rebut(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oImpagat As DTOImpagat = _Impagats.First

        If FEB.Impagat.Load(oImpagat, exs) Then
            If FEB.Contact.Load(oImpagat.Csb.Contact, exs) Then
                Dim oRebut As New DTORebut(oImpagat.Csb.Contact.Lang,
                                   oImpagat.Csb.FormattedId(),
                                   DTOImpagat.PendentAmbGastos(oImpagat),
                                   oImpagat.Csb.Csa.Fch,
                                   oImpagat.Csb.Vto,
                                   oImpagat.Csb.Contact.Nom,
                                   oImpagat.Csb.Contact.Address.Text,
                                   DTOAddress.ZipyCit(oImpagat.Csb.Contact.Address),
                                   oImpagat.Csb.Txt,
                                   oImpagat.Csb.Iban.Digits)

                Dim oFrm As New Frm_Rebut(oRebut)
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = Nothing
        Dim oImpagat As DTOImpagat = _Impagats.First
        If FEB.Impagat.Load(oImpagat, exs) Then
            oContact = _Impagats(0).Csb.Contact
            Dim oExercici As DTOExercici = DTOExercici.Current(Current.Session.Emp)
            Dim oCta = Await FEB.PgcCta.FromCod(DTOPgcPlan.Ctas.impagats, Current.Session.Emp, exs)
            Dim oFrm As New Frm_Extracte(oContact, oCta, oExercici)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Cobrar()
        MsgBox("Ojo, no implementat encara")

        Dim oFrm As New Frm_Cobrament
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            '.Impagats = _Impagats
            '.Show()
        End With
        'oFrm.Show()
    End Sub

    Private Async Sub Do_MailImpagat(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oImpagat As DTOImpagat = _Impagats.First
        Dim exs As New List(Of Exception)
        Dim oMailMessage = Await FEB.ImpagatMsg.MaiMessage(oImpagat, exs)
        If exs.Count = 0 Then
            If Not Await OutlookHelper.Send(oMailMessage, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs, "error al redactar missatge")
        End If
    End Sub


    Private Sub Do_CopyTpv(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oImpagat As DTOImpagat = _Impagats.First
        Dim url As String = FEB.Impagat.UrlTpv(oImpagat, True)
        Clipboard.SetDataObject(url, True)
    End Sub

    '==========================================================================
    '                               EVENT TRIGGERS
    '==========================================================================


End Class
