Public Class Xl_LocationsCombo

    Private _values As List(Of Location)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of Location), Optional oSelectedArea As area = Nothing)
        _ControlItems = New ControlItems
        _ControlItems.Add(ControlItem.AllLocations)
        For Each oLocation As Location In values
            Dim oControlItem As New ControlItem(oLocation)
            _ControlItems.Add(oControlItem)
        Next
        With ComboBox1
            .DataSource = _ControlItems
            .DisplayMember = "Nom"
        End With

        'set selected Location
        If oSelectedArea IsNot Nothing Then
            For i As Integer = 1 To _ControlItems.Count - 1
                Dim oLocation As Location = oSelectedArea.location
                If _ControlItems(i).Source.Guid.Equals(oSelectedArea.Guid) Then
                    ComboBox1.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As Location
        Get
            Dim retval As Location = Nothing
            Dim oItem As ControlItem = ComboBox1.SelectedItem
            If oItem IsNot Nothing Then
                retval = oItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As Location

        Public Property Nom As String

        Public Sub New(Optional oLocation As Location = Nothing)
            MyBase.New()
            If oLocation IsNot Nothing Then
                _Source = oLocation
                With _Source
                    _Nom = .Nom
                End With
            End If
        End Sub

        Shared Function AllLocations() As ControlItem
            Dim retval As New ControlItem
            retval.Nom = BLL.BLLSession.Current.Lang.Tradueix("(todas las poblaciones)", "(totes les poblacions)", "(any location)")
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class


End Class

