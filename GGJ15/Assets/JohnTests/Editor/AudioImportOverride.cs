using UnityEngine;
using UnityEditor;

public class AudioImportOverride : AssetPostprocessor {
	void OnPreprocessAudio () {
		AudioImporter importer = (AudioImporter) assetImporter;
		string name = importer.assetPath;
		if(name.LastIndexOf("Resources/Audio")>=0){
			importer.threeD = false;
			importer.loadType = AudioImporterLoadType.DecompressOnLoad;
			Debug.Log("Audio to source location");
		}
	}
}