Public Class Xl_LookupAddress

    Inherits Xl_LookupTextboxButton

    Private _Address As DTOAddress
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public ReadOnly Property Address() As DTOAddress
        Get
            Return _Address
        End Get
    End Property

    Public Shadows Sub Load(oAddress As DTOAddress)
        If oAddress Is Nothing Then
            _Address = New DTOAddress
        Else
            _Address = oAddress
        End If

        refresca()
        MyBase.AllowDrop = True
    End Sub

    Public Sub Clear()
        _Address = Nothing
        refresca()
    End Sub

    Private Sub Xl_LookupAddress_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Address(_Address)
        AddHandler oFrm.AfterUpdate, AddressOf onAddressUpdate
        oFrm.Show()
    End Sub

    Private Sub onAddressUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
        _Address = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Address Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            Dim oLang As DTOLang = Current.Session.Lang
            MyBase.Text = DTOAddress.ReverseSingleLine(_Address, oLang)
            Dim oMenu_Address As New Menu_Adr(_Address)
            AddHandler oMenu_Address.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Address.Range)
        End If
    End Sub

    Private Sub MyBaseDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.StringFormat) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Async Sub MyBaseDragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Dim src As String = e.Data.GetData(DataFormats.StringFormat)
        Dim exs As New List(Of Exception)
        Dim oAddress = Await FEB.Address.FromString(src, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Address(oAddress)
            AddHandler oFrm.AfterUpdate, AddressOf onAddressUpdate
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class
