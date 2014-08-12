Imports net.openstack.Core.Domain
Imports net.openstack.Core.Providers

Public Class ComputeExample

    Public Sub ExamineImages(provider As IComputeProvider)

        Dim images = ListAllImagesSince(provider, DateTimeOffset.Now - TimeSpan.FromHours(24))
        For Each image In images
            If image.Status Is ImageState.Active Then
                If image.Progress = 100 Then
                    ' Successful image; ignore this case.
                Else
                    ' TODO: In really shouldn't be active? Not sure how to handle.
                End If
            ElseIf image.Status Is ImageState.Error OrElse image.Status Is ImageState.Unknown Then
                ' TODO: send email for this image here
            ElseIf image.Status Is ImageState.Saving Then
                ' Image is saving (i.e. not yet complete; handle next time)
            ElseIf image.Status Is ImageState.Deleted Then
                ' Could occur since changesSince is specified. Probably want to ignore.
            Else
                ' TODO: Some unrecognized/undocumented status was returned. Not sure how to handle.
            End If
        Next

    End Sub

    Private Function ListAllImagesSince(provider As IComputeProvider, changesSince As DateTimeOffset, Optional region As String = Nothing, Optional identity As CloudIdentity = Nothing) As ServerImage()
        Dim images = New List(Of ServerImage)

        Dim lastImage As ServerImage = Nothing

        Do
            Dim marker = If(lastImage IsNot Nothing, lastImage.Id, Nothing)
            Dim page = provider.ListImagesWithDetails(changesSince:=changesSince, markerId:=marker, region:=region, identity:=identity)
            lastImage = Nothing
            For Each image In images
                lastImage = image
                images.Add(image)
            Next
        Loop While lastImage IsNot Nothing

        Return images.ToArray
    End Function

End Class
