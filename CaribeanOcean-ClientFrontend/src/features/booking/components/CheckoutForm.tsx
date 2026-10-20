import React, { useState } from 'react';
import styles from './Booking.module.css';
import type { RoomType, GuestData, ReservationPriceResult } from '../types/booking.types';


interface Props {
  selectedRoom: RoomType;
  guestData: GuestData;
  isPriceLoading: boolean;
  priceError: string | null;
  pricePreview: ReservationPriceResult | null;
  updateGuestField: (field: keyof GuestData, value: string) => void;
  onAccept: (e: React.SubmitEvent) => void;
  onCancel: () => void;
}


export const CheckoutForm = ({ isPriceLoading, priceError, pricePreview, selectedRoom, guestData, updateGuestField, onAccept, onCancel }: Props) => {
  const [showPriceDetails, setShowPriceDetails] = useState(false);
  return (
    <div className={styles.checkoutHero}>
      <form className={styles.checkoutContainer} onSubmit={onAccept}>
        <h2 className={styles.title}>Reservation</h2>

        <div className={styles.checkoutHeader}>
          <h3>Room: {selectedRoom.name}</h3>
          {/* El bloque de precio se va a la derecha */}
          <div className={styles.priceBlock}>
            {isPriceLoading ? (
              <p className={styles.priceCalculating}>Calculating price...</p>
            ) : priceError ? (
              <p className={styles.priceDetails} style={{ color: '#D85A30' }}>
                Price unavailable
              </p>
            ) : pricePreview ? (
              <>
                <div className={styles.priceTotal}>
                  Total: ${pricePreview.totalAmount.toFixed(2)}
                </div>
                <p className={styles.priceDetails}>
                  {pricePreview.nightCount} nights · Avg. ${pricePreview.averageNightlyRate.toFixed(2)} / night
                </p>

                <button
                  type="button"
                  className={styles.priceDetailsButton}
                  onClick={() => setShowPriceDetails((prev) => !prev)}
                >
                  {showPriceDetails ? "Hide price details" : "View price details"}
                </button>
              </>
            ) : (
              <div className={styles.priceTotal}>Total: --</div>
            )}
          </div>
        </div>

        {priceError && (
          <div className={styles.errorMessageContainer}>
            <span className={styles.errorIcon}>⚠</span>
            <span>{priceError}</span>
          </div>
        )}

        {/* Precio por noche detallado */}
        {pricePreview && showPriceDetails && (
          <div className={styles.priceDetailsBox}>
            <h4>Price breakdown</h4>

            {pricePreview.nights.map((night) => (
              <div key={night.stayDate} className={styles.priceDetailRow}>
                <div>
                  <strong>
                    {new Date(night.stayDate).toLocaleDateString()}
                  </strong>

                  <p>
                    Base ${night.basePrice.toFixed(2)}
                    {night.seasonName && (
                      <> · {night.seasonName} ({night.seasonAdjustmentPercentage}%)</>
                    )}
                    {night.offerName && (
                      <> · {night.offerName} (-{night.offerDiscountPercentage}%)</>
                    )}
                  </p>
                </div>

                <span>${night.finalNightPrice.toFixed(2)}</span>
              </div>
            ))}
          </div>
        )}
        <div className={styles.checkoutImagePlaceholder}>
          {selectedRoom.imageUrl ? (
            <img
              src={selectedRoom.imageUrl}
              alt={selectedRoom.name}
              className={styles.checkoutImage}
            />
          ) : (
            <span>Image Placeholder</span>
          )}
        </div>

        <div className={styles.checkoutFormGrid}>
          <div className={styles.inputGroup} style={{ alignItems: 'flex-start' }}>
            <label htmlFor="name">Name</label>
            <input
              type="text"
              id="name"
              value={guestData.guestName}
              onChange={(e) => updateGuestField('guestName', e.target.value)}
              required
            />
          </div>

          <div className={styles.inputGroup} style={{ alignItems: 'flex-start' }}>
            <label htmlFor="lastName">Last Name</label>
            <input
              type="text"
              id="lastName"
              value={guestData.guestLastName}
              onChange={(e) => updateGuestField('guestLastName', e.target.value)}
              required
            />
          </div>

          <div className={styles.inputGroup} style={{ alignItems: 'flex-start' }}>
            <label htmlFor="email">Email</label>
            <input
              type="email"
              id="email"
              value={guestData.guestEmail}
              onChange={(e) => updateGuestField('guestEmail', e.target.value)}
              required
            />
          </div>

          <div className={styles.inputGroup} style={{ alignItems: 'flex-start' }}>
            <label htmlFor="creditCard">Credit Card</label>
            <input
              type="text"
              id="creditCard"
              value={guestData.creditCard}
              onChange={(e) => updateGuestField('creditCard', e.target.value)}
              placeholder="XXXX-XXXX-XXXX-XXXX"
              required
            />
          </div>
        </div>

        <div className={styles.checkoutActions}>
          <button
            type="button"
            className={styles.cancelButton}
            onClick={onCancel}
          >
            Cancel
          </button>

          <button
            type="submit"
            className={styles.acceptButton}
            disabled={isPriceLoading || !!priceError || !pricePreview}
          >
            Accept
          </button>
        </div>
      </form>
    </div>
  );
};