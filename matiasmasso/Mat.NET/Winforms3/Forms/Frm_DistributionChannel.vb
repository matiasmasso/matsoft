Public Class Frm_DistributionChannel
    Private _DistributionChannel As DTODistributionChannel
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Classes
        Products
        Dtos
    End Enum

    Public Sub New(value As DTODistributionChannel)
        MyBase.New()
        Me.InitializeComponent()
        _DistributionChannel = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.DistributionChannel.Load(_DistributionChannel, exs) Then
            With _DistributionChannel
                TextBoxNomEsp.Text = .LangText.Esp
                TextBoxNomCat.Text = .LangText.Cat
                TextBoxNomEng.Text = .LangText.Eng
                NumericUpDownOrd.Value = .Ord
                NumericUpDownConsumerPriority.Value = .ConsumerPriority
                Xl_ContactClasses1.Load(.ContactClasses)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomEsp.TextChanged,
         TextBoxNomCat.TextChanged,
          TextBoxNomEng.TextChanged,
           NumericUpDownOrd.ValueChanged,
            NumericUpDownConsumerPriority.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _DistributionChannel
            .langText = New DTOLangText(TextBoxNomEsp.Text, TextBoxNomCat.Text, TextBoxNomEng.Text)
            .ord = NumericUpDownOrd.Value
            .consumerPriority = NumericUpDownConsumerPriority.Value
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.DistributionChannel.Update(_DistributionChannel, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_DistributionChannel))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.DistributionChannel.Delete(_DistributionChannel, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_DistributionChannel))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub Xl_ContactClasses1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ContactClasses1.RequestToAddNew
        Dim oFrm As New Frm_ContactClasses(Nothing, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.ItemSelected, AddressOf onContactClassSelected
        oFrm.Show()
    End Sub

    Private Async Sub onContactClassSelected(sender As Object, e As MatEventArgs)
        Dim oContactClass As DTOContactClass = e.Argument
        oContactClass.DistributionChannel = _DistributionChannel
        Dim exs As New List(Of Exception)
        If Await FEB.ContactClass.Update(oContactClass, exs) Then
            _DistributionChannel.ContactClasses.Add(oContactClass)
            _DistributionChannel.IsLoaded = False
            If FEB.DistributionChannel.Load(_DistributionChannel, exs) Then
                Xl_ContactClasses1.Load(_DistributionChannel.ContactClasses)
                ButtonOk.Enabled = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Dtos
                Await LoadDtos()
            Case Tabs.Products
                Await LoadProducts()
        End Select
    End Sub

#Region "Products"
    Private Async Function LoadProducts() As Task
        Dim exs As New List(Of Exception)
        Dim oProductChannels = Await FEB.ProductChannels.All(exs, _DistributionChannel)
        If exs.Count = 0 Then
            Xl_ProductChannels1.Load(oProductChannels, Xl_ProductChannels.Modes.ProductsPerChannel)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub RefrescaProducts(sender As Object, e As MatEventArgs)
        Await LoadProducts()
    End Sub
    Private Async Function RefrescaProducts() As Task
        Await LoadProducts()
    End Function

    Private Sub Xl_ProductChannels1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductChannels1.RequestToAddNew
        Dim item As New DTOProductChannel()
        With item
            .DistributionChannel = _DistributionChannel
            .Cod = DTOProductChannel.Cods.Inclou
        End With
        Dim oFrm As New Frm_ProductChannel(item)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaProducts
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductChannels1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductChannels1.RequestToRefresh
        Await RefrescaProducts()
    End Sub
#End Region

#Region "Dtos"
    Private Async Function LoadDtos() As Task
        Dim exs As New List(Of Exception)
        Dim oDtos = Await FEB.CustomerTarifaDtos.All(exs, _DistributionChannel)
        If exs.Count = 0 Then
            Xl_CustomerDtos1.Load(oDtos, HideSrc:=True)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub RefrescaDtos(sender As Object, e As MatEventArgs)
        Await RefrescaDtos()
    End Sub

    Private Async Function RefrescaDtos() As Task
        Await LoadDtos()
    End Function

    Private Sub Xl_CustomerDtos1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CustomerDtos1.RequestToAddNew
        Dim oCustomerDto = DTOCustomerTarifaDto.Factory(_DistributionChannel)
        Dim oFrm As New Frm_CustomerDto(oCustomerDto)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaDtos
        oFrm.Show()
    End Sub

    Private Async Sub Xl_CustomerDtos1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CustomerDtos1.RequestToRefresh
        Await RefrescaDtos()
    End Sub
#End Region

End Class


