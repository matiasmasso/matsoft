Public Class Menu_Country
    Inherits Menu_Base

    Private _Country As DTOCountry
    Private _Countries As List(Of DTOCountry)

    Public Sub New(ByVal oCountry As DTOCountry)
        MyBase.New()
        _Country = oCountry
        _Countries = New List(Of DTOCountry)
        _Countries.Add(_Country)
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oCountries As List(Of DTOCountry))
        MyBase.New()
        _Countries = oCountries
        _Country = _Countries.First
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Sellout())
        MyBase.AddMenuItem(MenuItem_IbanStructure())
    End Sub

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

    Private Function MenuItem_Sellout() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sellout"
        AddHandler oMenuItem.Click, AddressOf Do_Sellout
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
        AddHandler oFrm.AfterDelete, AddressOf DeleteRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Sellout(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSellout = Await FEB.SellOut.Factory(exs, Current.Session.User,  , DTOSellOut.ConceptTypes.Yeas, DTOStat.Formats.Units)
        If exs.Count = 0 Then
            FEB.SellOut.AddFilterValues(oSellout, DTOSellOut.Filter.Cods.Atlas, _Countries.ToArray)
            Dim oFrm As New Frm_SellOut(oSellout)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_IbanStructure(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oIbanStructure = Await FEB.IbanStructure.Find(_Country, exs)
        If exs.Count = 0 Then
            If oIbanStructure Is Nothing Then
                oIbanStructure = DTOIban.Structure.Factory(_Country)
                oIbanStructure.IsNew = True
            End If

            Dim oFrm As New Frm_IbanStructure(oIbanStructure)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class
