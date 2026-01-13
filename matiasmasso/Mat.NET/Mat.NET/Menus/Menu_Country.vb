Public Class Menu_Country
    Inherits Menu_Base

    Private _Country As DTOCountry

    Public Sub New(ByVal oCountry As DTOCountry)
        MyBase.New()
        _Country = oCountry
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
                                         MenuItem_IbanStructure()})
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

    Private Function MenuItem_IbanStructure() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Estructura Iban"
        AddHandler oMenuItem.Click, AddressOf Do_IbanStructure
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Country(_Country)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_IbanStructure(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oIbanStructure As DTOIbanStructure = BLL.BLLIbanStructure.FindOrDefault(_Country)
        Dim oFrm As New Frm_IbanStructure(oIbanStructure)
        oFrm.Show()
    End Sub


End Class
