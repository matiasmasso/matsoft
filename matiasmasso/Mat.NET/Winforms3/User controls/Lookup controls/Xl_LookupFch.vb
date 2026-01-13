Public Class Xl_LookupFch
    Inherits Xl_LookupTextboxButton

    Private _Fch As Date
    Private _AvailableFchs As List(Of Date)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Fch() As Date
        Get
            Return _Fch
        End Get
        Set(ByVal value As Date)
            _Fch = value
            If _Fch = Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = String.Format("{0:dd/MM/yy}", _Fch)
            End If
        End Set
    End Property

    Public Property AvailableFchs As List(Of Date)
        Get
            If _AvailableFchs Is Nothing Then _AvailableFchs = New List(Of Date)
            Return _AvailableFchs
        End Get
        Set(value As List(Of Date))
            _AvailableFchs = value
        End Set
    End Property

    Public Sub Clear()
        Me.Fch = Nothing
    End Sub

    Private Sub Xl_LookupBanc_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_MonthCalendar(_Fch, _AvailableFchs)
        AddHandler oFrm.AfterUpdate, AddressOf onDateChanged
        oFrm.Show()
    End Sub

    Private Sub onDateChanged(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oDateRange As SelectionRange = e.Argument
        Me.Fch = oDateRange.End
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Fch))
    End Sub

End Class
