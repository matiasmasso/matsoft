

Public Class Menu_Impagat
    Private mImpagat As Impagat
    Private mImpagats As Impagats

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oImpagat As Impagat)
        MyBase.New()
        mImpagat = oImpagat
        mImpagats = New Impagats
        mImpagats.Add(mImpagat)
    End Sub

    Public Sub New(ByVal oImpagats As Impagats)
        MyBase.New()
        mImpagats = oImpagats
        mImpagat = oImpagats(0)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Extracte(), _
        MenuItem_Contact(), _
        MenuItem_Rebut(), _
        MenuItem_Cobrar(), _
        MenuItem_Insolvencia()})
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
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Contacte..."
        If mImpagat.Csb.Client Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.DropDownItems.AddRange(New Menu_Contact(mImpagat.Csb.Client).Range)
        End If
        oMenuItem.Image = My.Resources.People_Blue
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

    Private Function MenuItem_Insolvencia() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Insolvencia"
        oMenuItem.Image = My.Resources.cyc
        AddHandler oMenuItem.Click, AddressOf Do_Insolvencia
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Impagat
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Impagat = mImpagat
            .Show()
        End With
    End Sub

    Private Sub Do_Rebut(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowRebut(mImpagat.Csb.Rebut)
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContact As Contact = mImpagat.Csb.Client
        Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.impagats)
        Dim oFrm As New Frm_CliCtas(oContact, oCta)
        oFrm.Show()
    End Sub

    Private Sub Do_Cobrar()
        Dim oFrm As New Frm_Cobrament
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .impagats = mImpagats
            .Show()
        End With
        oFrm.Show()
    End Sub

    Private Sub Do_Insolvencia()
        Dim oContact As Contact = Nothing
        Dim oImpagat As Impagat = Nothing
        Dim oNominal As New maxisrvr.Amt
        Dim oGastos As New maxisrvr.Amt

        For Each oImpagat In mImpagats
            oNominal.Add(oImpagat.Pendent)
            oGastos.Add(oImpagat.Gastos)
        Next

        Dim oTmp As maxisrvr.Amt = oNominal.Clone
        oTmp.Add(oGastos)
        Dim oComisio As maxisrvr.Amt = oTmp.Percent(20)

        oContact = mImpagats(0).Csb.Client

        Dim oInsolvencia As New Insolvencia(oContact)
        With oInsolvencia
            .Nominal = oNominal
            .Gastos = oGastos
            .Comisio = oComisio
            .Impagats = mImpagats
            .FchPresentacio = Today
        End With

        Dim oFrm As New Frm_Insolvencia
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Insolvencia = oInsolvencia
            .Show()
        End With

    End Sub


    '==========================================================================
    '                               EVENT TRIGGERS
    '==========================================================================

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
