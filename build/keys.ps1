# Note: these values may only change during minor release
$Keys = @{
	'v3.5' = '60ee1d542341ddcd'
	'v4.0' = '326b3de041f03d04'
}

function Resolve-FullPath() {
	param([string]$Path)
	[System.IO.Path]::GetFullPath((Join-Path (pwd) $Path))
}
