using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runtime.Game.UserAccountSystem
{
    public class GalleryPickerService
    {
        public async UniTask<Sprite> PickFromGallery(int maxSize, CancellationToken cancellationToken = default)
        {
            var tcs = new UniTaskCompletionSource<Sprite>();

            NativeGallery.GetImageFromGallery(path =>
            {
                Sprite result = null;

                if (!string.IsNullOrEmpty(path))
                {
                    var texture = NativeGallery.LoadImageAtPath(path, maxSize);
                    if (texture != null)
                    {
                        result = Sprite.Create(
                            texture,
                            new Rect(0, 0, texture.width, texture.height),
                            Vector2.zero
                        );
                    }
                }

                tcs.TrySetResult(result);
            });

            return await tcs.Task.AttachExternalCancellation(cancellationToken);
        }
    }
}
