# Piwigo.SDK
I created this project to be able to work with the amazing gallery of Piwigo from .NET Core MVC.
I am adding methods as needed, but if you want you help and add more functionality you are more than welcome.

## Update Piwigo PHP
Navigate to /include/ws_functions and update pwg.images.php
Around line 1286 in the method ws_images_addSimple, add this code:

```text
// My Update
  $query = '
  SELECT path 
  FROM '. IMAGES_TABLE .'
  where id = '. $image_id .';';
  $result = pwg_query($query);
  $urlPath = pwg_db_fetch_assoc($result);
  //----------
  
  
  // update metadata from the uploaded file (exif/iptc), even if the sync
  // was already performed by add_uploaded_file().
  require_once(PHPWG_ROOT_PATH.'admin/include/functions_metadata.php');
  sync_metadata(array($image_id));

  return array(
    'image_id' => $image_id,
    //'url' => make_picture_url($url_params),
	'url' => $urlPath,
    );
}
```

It will return the picture url directly, so you can stored in the database.