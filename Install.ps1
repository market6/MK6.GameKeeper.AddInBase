param($installPath, $toolsPath, $package, $project)

foreach ($reference in $project.Object.References)
{
    if($reference.Name -eq "MK6.GameKeeper.AddIns")
    {
        $reference.CopyLocal = $false;
    }
}