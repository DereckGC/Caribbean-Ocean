import { useEffect, useState } from "react";
import { getGalleryImages } from "../services/galery.service";
import type { GalleryImage } from "../types/gallery.types";

export function useGallery() {
    const [imgs, setImgs] = useState<GalleryImage[]>([]); 
    const [selectedImageId, setSelectedImageId] = useState<number | null>(null);

    const mainImage = imgs.find(img => img.id === selectedImageId) || imgs[0];
    const thumbnails = imgs.filter(img => img.id !== (mainImage?.id));

    useEffect(() => {
        getGalleryImages()
            .then((res) => {
                setImgs(res);
                if (res.length > 0) setSelectedImageId(res[0].id);
            })
            .catch((error) => console.error(error));
    }, []);

    return { imgs, mainImage, thumbnails, setSelectedImageId };
}