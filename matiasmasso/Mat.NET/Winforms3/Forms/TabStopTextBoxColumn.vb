Public Class TabStopTextBoxColumn
    Inherits DataGridViewTextBoxColumn

    Public Sub New()
        Me.CellTemplate = New TabStopTextBoxCell
    End Sub

    Public Overrides Property CellTemplate() As System.Windows.Forms.DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As System.Windows.Forms.DataGridViewCell)
            If Not (value Is Nothing) AndAlso _
                Not value.GetType().IsAssignableFrom(GetType(TabStopTextBoxCell)) _
                Then
                Throw New InvalidCastException("Must be a TabStopTextBoxCell")
            End If
            MyBase.CellTemplate = value
        End Set
    End Property

    Public Property TabStop() As Boolean
        Get
            Return DirectCast(MyBase.CellTemplate, TabStopTextBoxCell).TabStop
        End Get
        Set(ByVal value As Boolean)
            DirectCast(MyBase.CellTemplate, TabStopTextBoxCell).TabStop = value
        End Set
    End Property

    Public Overrides Function Clone() As Object
        Return MyBase.Clone()
    End Function
End Class

Public Class TabStopImageColumn
    Inherits DataGridViewImageColumn

    Public Sub New(valuesareIcons As Boolean)
        MyBase.New(valuesareIcons)
        Me.CellTemplate = New DataGridViewImageCellBlank
    End Sub

    Public Overrides Property CellTemplate() As System.Windows.Forms.DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As System.Windows.Forms.DataGridViewCell)
            If Not (value Is Nothing) AndAlso
                Not value.GetType().IsAssignableFrom(GetType(DataGridViewImageCellBlank)) _
                Then
                Throw New InvalidCastException("Must be a DataGridViewImageCellBlank")
            End If
            MyBase.CellTemplate = value
        End Set
    End Property

    Public Property TabStop() As Boolean
        Get
            Return DirectCast(MyBase.CellTemplate, DataGridViewImageCellBlank).TabStop
        End Get
        Set(ByVal value As Boolean)
            DirectCast(MyBase.CellTemplate, DataGridViewImageCellBlank).TabStop = value
        End Set
    End Property

    Public Overrides Function Clone() As Object
        Return MyBase.Clone()
    End Function
End Class
