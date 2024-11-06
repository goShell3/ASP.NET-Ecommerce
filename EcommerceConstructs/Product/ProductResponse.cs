public record ProductResponse(
    Guid Id,
    string product_id,
    string name,
    string description,
    decimal price,
    string currency,
    List<ProductCategory> product_categories,
    string brand,
    Stock stock,
    List<ProductSpecification> product_specifications,
    ProductRating product_rating,
    List<ProductImage> product_images,
    List<RelatedProduct> related_products,
    DateTime created_at,
    DateTime updated_at
);

public record ProductCategory (
    string category_id,
    string name
);

public record Stock (
    string availability,
    string quantity
);

public record ProductSpecification (
    string display,
    string processor,
    string ram,
    string storage,
    string battery
);

public record ProductRating (
    decimal average,
    int review_count
);

public record ProductImage (
    string image_id,
    string image_url,
    string alt_text 
);

public record RelatedProduct (
    string product_id,
    string name,
    decimal price
);
