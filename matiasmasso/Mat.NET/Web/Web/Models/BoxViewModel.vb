Public Class BoxViewModel
    Property Title As String
    Property Text As String
    Property NavigateTo As String
    Property Tag As String

    Shared Function Factory(title As String, Optional navigateTo As String = "", Optional text As String = "", Optional tag As String = "") As BoxViewModel
        Dim retval As New BoxViewModel
        With retval
            .Title = title
            .NavigateTo = navigateTo
            .Text = text
            .Tag = tag
        End With
        Return retval
    End Function

    Public Class Collection
        Inherits List(Of BoxViewModel)

        Public Overloads Function Add(title As String, Optional navigateTo As String = "", Optional text As String = "", Optional tag As String = "") As BoxViewModel
            Dim retval = BoxViewModel.Factory(title, navigateTo, text, tag)
            MyBase.Add(retval)
            Return retval
        End Function
    End Class

End Class

Public Class ImageBoxViewModel
    Inherits BoxViewModel

    Property ImageUrl As String
    Property ImageWidth As Integer
    Property ImageHeight As Integer

    Overloads Shared Function Factory(imageUrl As String, imageWidth As Integer, imageHeight As Integer, title As String, Optional navigateTo As String = "", Optional text As String = "", Optional tag As String = "") As BoxViewModel
        Dim retval As New ImageBoxViewModel
        With retval
            .ImageUrl = imageUrl
            .ImageWidth = imageWidth
            .ImageHeight = imageHeight
            .Title = title
            .NavigateTo = navigateTo
            .Text = text
            .Tag = tag
        End With
        Return retval
    End Function

    Public Shadows Class Collection
        Inherits List(Of ImageBoxViewModel)

        Shadows Function Add(imageUrl As String, imageWidth As Integer, imageHeight As Integer, title As String, Optional navigateTo As String = "", Optional text As String = "", Optional tag As String = "") As BoxViewModel
            Dim retval = ImageBoxViewModel.Factory(imageUrl, imageWidth, imageHeight, title, navigateTo, text, tag)
            MyBase.Add(retval)
            Return retval
        End Function

        Shadows Function Add(title As String, Optional navigateTo As String = "", Optional text As String = "", Optional tag As String = "") As BoxViewModel
            Dim retval = ImageBoxViewModel.Factory(title, navigateTo, text, tag)
            MyBase.Add(retval)
            Return retval
        End Function

    End Class


End Class
