Imports System.ComponentModel
Imports System.Reflection

Public Class SortableBindingList(Of T)
    Inherits BindingList(Of T)
    Private sortedList As ArrayList
    Private unsortedItems As ArrayList
    Private isSortedValue As Boolean

    Public Sub New()
    End Sub

    Public Sub New(list As IList(Of T))
        For Each o As Object In list
            Me.Add(DirectCast(o, T))
        Next
    End Sub

    Protected Overrides ReadOnly Property SupportsSearchingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides Function FindCore(prop As PropertyDescriptor, key As Object) As Integer
        Dim propInfo As PropertyInfo = GetType(T).GetProperty(prop.Name)
        Dim item As T

        If key IsNot Nothing Then
            For i As Integer = 0 To Count - 1
                item = DirectCast(Items(i), T)
                If propInfo.GetValue(item, Nothing).Equals(key) Then
                    Return i
                End If
            Next
        End If
        Return -1
    End Function

    Public Function Find([property] As String, key As Object) As Integer
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim prop As PropertyDescriptor = properties.Find([property], True)

        If prop Is Nothing Then
            Return -1
        Else
            Return FindCore(prop, key)
        End If
    End Function

    Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
        Get
            Return True
        End Get
    End Property


    Protected Overrides ReadOnly Property IsSortedCore() As Boolean
        Get
            Return isSortedValue
        End Get
    End Property

    Private sortDirectionValue As ListSortDirection
    Private sortPropertyValue As PropertyDescriptor

    Protected Overrides Sub ApplySortCore(prop As PropertyDescriptor, direction As ListSortDirection)
        sortedList = New ArrayList()

        Dim interfaceType As Type = prop.PropertyType.GetInterface("IComparable")

        If interfaceType Is Nothing AndAlso prop.PropertyType.IsValueType Then
            Dim underlyingType As Type = Nullable.GetUnderlyingType(prop.PropertyType)

            If underlyingType IsNot Nothing Then
                interfaceType = underlyingType.GetInterface("IComparable")
            End If
        End If

        If interfaceType IsNot Nothing Then
            sortPropertyValue = prop
            sortDirectionValue = direction

            Dim query As IEnumerable(Of T) = MyBase.Items
            If direction = ListSortDirection.Ascending Then
                query = query.OrderBy(Function(i) prop.GetValue(i))
            Else
                query = query.OrderByDescending(Function(i) prop.GetValue(i))
            End If
            Dim newIndex As Integer = 0
            For Each item As Object In query
                Me.Items(newIndex) = DirectCast(item, T)
                newIndex += 1
            Next
            isSortedValue = True

            Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
        Else
            Throw New NotSupportedException("Cannot sort by " + prop.Name + ". This" + prop.PropertyType.ToString() + " does not implement IComparable")
        End If
    End Sub

    Protected Overrides Sub RemoveSortCore()
        Dim position As Integer
        Dim temp As Object

        If unsortedItems IsNot Nothing Then
            Dim i As Integer = 0
            While i < unsortedItems.Count
                position = Me.Find("LastName", unsortedItems(i).[GetType]().GetProperty("LastName").GetValue(unsortedItems(i), Nothing))
                If position > 0 AndAlso position <> i Then
                    temp = Me(i)
                    Me(i) = Me(position)
                    Me(position) = DirectCast(temp, T)
                    i += 1
                ElseIf position = i Then
                    i += 1
                Else
                    unsortedItems.RemoveAt(i)
                End If
            End While
            isSortedValue = False
            OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
        End If
    End Sub

    Public Sub RemoveSort()
        RemoveSortCore()
    End Sub
    Protected Overrides ReadOnly Property SortPropertyCore() As PropertyDescriptor
        Get
            Return sortPropertyValue
        End Get
    End Property

    Protected Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
        Get
            Return sortDirectionValue
        End Get
    End Property

End Class

